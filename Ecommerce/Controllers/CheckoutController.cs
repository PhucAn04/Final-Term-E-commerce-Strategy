using Ecommerce.Data;
using Ecommerce.Helpers;
using Ecommerce.Models;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;
using Newtonsoft.Json;

namespace Ecommerce.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PaypalClient _paypalClient;

        public CheckoutController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, PaypalClient paypalClient)
        {
            db = context;
            _userManager = userManager;
            _paypalClient = paypalClient;
        }

        // Giỏ hàng lấy từ Session
        public List<CartItemViewModel> Cart => HttpContext.Session.Get<List<CartItemViewModel>>(MySetting.CART_KEY) ?? new List<CartItemViewModel>();

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            if (Cart.Count == 0)
            {
                TempData["Message"] = "Giỏ hàng của bạn đang trống!";
                return RedirectToAction("Index", "Product");
            }
            ViewBag.PaypalClientId = _paypalClient.ClientId;
            return View(Cart);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel model, string payment = "COD")
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var userId = user.Id;       // Lấy UserId từ Identity
                var username = user.UserName;

                // --- Xử lý Stripe ---
                if (payment == "Stripe")
                {
                    var carts = Cart;
                    if (!carts.Any()) return BadRequest("Giỏ hàng trống");

                    // Tự động lấy domain hiện tại
                    var domain = $"{Request.Scheme}://{Request.Host}";

                    // 1. Tạo danh sách sản phẩm cho Stripe
                    var lineItems = new List<SessionLineItemOptions>();
                    foreach (var item in carts)
                    {
                        lineItems.Add(new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount = (long)(item.donGia * 100), // Stripe tính theo cents
                                Currency = "usd", 
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = item.TenSp,
                                    Images = new List<string> { domain + "/Hinh/HangHoa/" + item.Hinh }
                                },
                            },
                            Quantity = item.soLuong,
                        });
                    }

                    // 2. Cấu hình Session Stripe
                    var options = new SessionCreateOptions
                    {
                        PaymentMethodTypes = new List<string> { "card" },
                        LineItems = lineItems,
                        Mode = "payment",
                        SuccessUrl = domain + "/Checkout/SuccessStripe?session_id={CHECKOUT_SESSION_ID}",
                        CancelUrl = domain + "/Checkout/PaymentFail",
                        Metadata = new Dictionary<string, string>
                        {
                            { "HoTen", model.HoTen ?? "" },
                            { "DiaChi", model.DiaChi ?? "" },
                            { "PhoneNumber", model.PhoneNumber ?? "" },
                            { "GhiChu", model.GhiChu ?? "" }
                        }
                    };

                    var service = new SessionService();
                    Session session = service.Create(options);

                    // 3. Chuyển hướng người dùng qua Stripe
                    return Redirect(session.Url);
                }

                // --- Xử lý COD/Mặc định ---
                // Tạo hóa đơn
                var hoadon = new THoaDonBan
                {
                    UserId = userId,
                    UserName = username,
                    HoTen = model.HoTen,
                    DiaChi = model.DiaChi,
                    PhoneNumber = model.PhoneNumber,
                    NgayDat = DateTime.Now,
                    CachThanhToan = "COD",
                    CachVanChuyen = "Giao hàng tận nơi",
                    MaTrangThai = 0, // trạng thái mặc định
                    TongTienHd = (decimal)Cart.Sum(item => item.soLuong * item.donGia),
                    GhiChu = model.GhiChu
                };

                using var transaction = db.Database.BeginTransaction();
                try
                {
                    db.Add(hoadon);
                    db.SaveChanges();

                    // Chi tiết hóa đơn
                    var cthds = new List<TChiTietHdb>();
                    foreach (var item in Cart)
                    {
                        cthds.Add(new TChiTietHdb
                        {
                            UserId = userId,
                            UserName = username,
                            MaHoaDon = hoadon.MaHoaDon,
                            SoLuongBan = item.soLuong,
                            DonGiaBan = (decimal)item.donGia,
                            MaSp = item.MaSp,
                            GiamGia = 0,
                            GhiChu = null
                        });
                    }

                    db.AddRange(cthds);
                    db.SaveChanges();

                    transaction.Commit();

                    // Xóa giỏ hàng sau khi checkout thành công
                    HttpContext.Session.Set<List<CartItemViewModel>>(MySetting.CART_KEY, new List<CartItemViewModel>());

                    return RedirectToAction("Index", "Product");
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }

            ViewBag.PaypalClientId = _paypalClient.ClientId;
            return View(Cart);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> SuccessStripe(string session_id)
        {
            var service = new SessionService();
            var session = service.Get(session_id);

            if (session.PaymentStatus == "paid")
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null) return RedirectToAction("Login", "Account");

                var userId = user.Id;
                var username = user.UserName;

                var carts = Cart;
                if (!carts.Any()) return RedirectToAction("Index");

                // Ưu tiên lấy từ Metadata (form nhập)
                string hoTen = session.Metadata.ContainsKey("HoTen") ? session.Metadata["HoTen"] : username;
                string diaChi = session.Metadata.ContainsKey("DiaChi") ? session.Metadata["DiaChi"] : "";
                string dienThoai = session.Metadata.ContainsKey("PhoneNumber") ? session.Metadata["PhoneNumber"] : "";
                string ghiChu = session.Metadata.ContainsKey("GhiChu") ? session.Metadata["GhiChu"] : "";

                var hoadon = new THoaDonBan
                {
                    UserId = userId,
                    UserName = username,
                    HoTen = hoTen,
                    DiaChi = diaChi,
                    PhoneNumber = dienThoai,
                    NgayDat = DateTime.Now,
                    CachThanhToan = "Stripe",
                    CachVanChuyen = "Giao hàng tận nơi",
                    MaTrangThai = 1, // Đã thanh toán
                    TongTienHd = (decimal)carts.Sum(item => item.soLuong * item.donGia),
                    GhiChu = ghiChu
                };

                db.Add(hoadon);
                db.SaveChanges();

                var cthds = carts.Select(item => new TChiTietHdb
                {
                    UserId = userId,
                    UserName = username,
                    MaHoaDon = hoadon.MaHoaDon,
                    SoLuongBan = item.soLuong,
                    DonGiaBan = (decimal)item.donGia,
                    MaSp = item.MaSp,
                    GiamGia = 0
                }).ToList();

                db.AddRange(cthds);
                db.SaveChanges();

                HttpContext.Session.Set<List<CartItemViewModel>>(MySetting.CART_KEY, new List<CartItemViewModel>());

                TempData["Message"] = "Thanh toán Stripe thành công!";
                return RedirectToAction("Index", "Product");
            }

            return RedirectToAction("PaymentFail");
        }

        #region Paypal payment
        [Authorize]
        [HttpPost("/Checkout/create-paypal-order")]
        public async Task<IActionResult> CreatePaypalOrder(CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest(new { message = "Bạn chưa đăng nhập" });
            }

            var carts = Cart;
            if (!carts.Any())
            {
                return BadRequest(new { message = "Giỏ hàng trống" });
            }

            var tongTien = carts.Sum(p => p.soLuong * p.donGia);
            var tongTienString = tongTien.ToString(System.Globalization.CultureInfo.InvariantCulture);

            var donViTienTe = "USD";
            var maDonHangThamChieu = "DH" + DateTime.Now.Ticks.ToString();

            try
            {
                var response = await _paypalClient.CreateOrder(tongTienString, donViTienTe, maDonHangThamChieu);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var error = new { ex.GetBaseException().Message };
                return BadRequest(error);
            }
        }

        [Authorize]
        [HttpPost("/Checkout/capture-paypal-order")]
        public async Task<IActionResult> CapturePaypalOrder(string orderID, CancellationToken cancellationToken, CheckoutViewModel model)
        {
            try
            {
                var response = await _paypalClient.CaptureOrder(orderID);
                if (response.status == "COMPLETED")
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user == null)
                    {
                        return Unauthorized("Bạn cần đăng nhập để hoàn tất thanh toán.");
                    }

                    var carts = Cart;
                    if (!carts.Any())
                    {
                        return BadRequest("Giỏ hàng trống.");
                    }

                    var userId = user.Id;
                    var username = user.UserName;

                    var hoadon = new THoaDonBan
                    {
                        UserId = userId,
                        UserName = username,
                        HoTen = model.HoTen ?? username,
                        DiaChi = model.DiaChi ?? "",
                        PhoneNumber = model.PhoneNumber ?? "",
                        NgayDat = DateTime.Now,
                        CachThanhToan = "Paypal",
                        CachVanChuyen = "Giao hàng tận nơi",
                        MaTrangThai = 1, // Đã thanh toán
                        TongTienHd = (decimal)carts.Sum(item => item.soLuong * item.donGia),
                        GhiChu = model.GhiChu
                    };

                    db.Add(hoadon);
                    db.SaveChanges();

                    var cthds = carts.Select(item => new TChiTietHdb
                    {
                        UserId = userId,
                        UserName = username,
                        MaHoaDon = hoadon.MaHoaDon,
                        SoLuongBan = item.soLuong,
                        DonGiaBan = (decimal)item.donGia,
                        MaSp = item.MaSp,
                        GiamGia = 0
                    }).ToList();

                    db.AddRange(cthds);
                    db.SaveChanges();

                    HttpContext.Session.Set<List<CartItemViewModel>>(MySetting.CART_KEY, new List<CartItemViewModel>());

                    return Ok(response);
                }
                else
                {
                    return BadRequest("Thanh toán PayPal không thành công.");
                }
            }
            catch (Exception ex)
            {
                var error = new { ex.GetBaseException().Message };
                return BadRequest(error);
            }
        }
        #endregion Paypal Payment

        [Authorize]
        public IActionResult PaymentFail()
        {
            TempData["Message"] = "Thanh toán thất bại hoặc đã bị hủy.";
            return RedirectToAction("Index", "Product"); 
        }
    }
}

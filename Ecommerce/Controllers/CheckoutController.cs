using Ecommerce.Data;
using Ecommerce.Helpers;
using Ecommerce.Models;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;

        public CheckoutController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            db = context;
            _userManager = userManager;
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
            return View(Cart);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
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

                    return RedirectToAction("Index", "Cart");
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return View(Cart);
        }
    }
}

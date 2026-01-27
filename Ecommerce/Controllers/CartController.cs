using Ecommerce.Data;
using Ecommerce.Helpers;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Authorize(Roles = "User")]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly ILogger<HomeController> _logger;

        public CartController(ApplicationDbContext _db, ILogger<HomeController> logger)
        {
            db = _db;
            _logger = logger;
        }

        public List<CartItemViewModel> Cart => HttpContext.Session.Get<List<CartItemViewModel>>(MySetting.CART_KEY) ?? new List<CartItemViewModel>();
        public IActionResult Index()
        {
            return View(Cart);
        }

        public IActionResult AddToCart(string id, int quantity = 1)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaSp == id);
            if (item == null)
            {
                var hangHoa = db.TDanhMucSps.SingleOrDefault(p => p.MaSp == id);
                if (hangHoa == null)
                {
                    TempData["Message"] = $"Không tìm thấy sản phẩm với mã {id}";
                    return NotFound();
                }
                item = new CartItemViewModel
                {
                    MaSp = hangHoa.MaSp,
                    tenSp = hangHoa.TenSp,
                    donGia = (double)(hangHoa.GiaLonNhat ?? 0),
                    hinh = hangHoa.AnhDaiDien,
                    soLuong = quantity
                };
                gioHang.Add(item);
            }
            else
            {
                item.soLuong += quantity;
            }

            HttpContext.Session.Set(MySetting.CART_KEY, gioHang);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveCart(string id)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaSp == id);
            if (item != null)
            {
                gioHang.Remove(item);
                HttpContext.Session.Set(MySetting.CART_KEY, gioHang);
            }
            else
            {
                TempData["Message"] = $"Không tìm thấy sản phẩm với mã {id}";
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Decrease(string id)
        {
            List<CartItemViewModel> cart = HttpContext.Session.Get<List<CartItemViewModel>>(MySetting.CART_KEY);

            CartItemViewModel cartItem = cart.SingleOrDefault(p => p.MaSp == id);

            if (cartItem.soLuong > 1)
            {
                --cartItem.soLuong;
            }
            else
            {
                cart.RemoveAll(p => p.MaSp == id);
            }
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove(MySetting.CART_KEY);
            }
            else
            {
                HttpContext.Session.Set(MySetting.CART_KEY, cart);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Increase(string id)
        {
            List<CartItemViewModel> cart = HttpContext.Session.Get<List<CartItemViewModel>>(MySetting.CART_KEY);

            CartItemViewModel cartItem = cart.SingleOrDefault(p => p.MaSp == id);

            if (cartItem.soLuong >= 1)
            {
                ++cartItem.soLuong;
            }
            else
            {
                cart.RemoveAll(p => p.MaSp == id);
            }
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove(MySetting.CART_KEY);
            }
            else
            {
                HttpContext.Session.Set(MySetting.CART_KEY, cart);
            }

            return RedirectToAction("Index");
        }
    }
}

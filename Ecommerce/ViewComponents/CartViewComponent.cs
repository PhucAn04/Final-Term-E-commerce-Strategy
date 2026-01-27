using Ecommerce.Helpers;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItemViewModel>>(MySetting.CART_KEY) ?? new List<CartItemViewModel>();

            return View("CartPanel", new CartViewModel
            {
                Quantity = cart.Sum(p => p.soLuong),
                Total = cart.Sum(p => p.thanhTien)
            });
        }
    }
}

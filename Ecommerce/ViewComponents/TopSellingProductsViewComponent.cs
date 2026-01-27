using Ecommerce.Data;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.ViewComponents
{
    public class TopSellingProductsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public TopSellingProductsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var topProducts = _context.TChiTietHdbs
                .AsNoTracking()
                .Include(ct => ct.MaSpNavigation)
                .GroupBy(ct => ct.MaSp)
                .Select(g => new TopSellingProductViewModel
                {
                    MaSp = g.Key,
                    TenSp = g.First().MaSpNavigation.TenSp,
                    AnhDaiDien = g.First().MaSpNavigation.AnhDaiDien,
                    GiaLonNhat = (decimal)g.First().MaSpNavigation.GiaLonNhat,
                    TotalSold = g.Sum(ct => ct.SoLuongBan ?? 0)
                })
                .Where(p => p.TotalSold > 0)
                .OrderByDescending(p => p.TotalSold)
                .Take(5)
                .ToList();

            return View(topProducts);
        }
    }
}
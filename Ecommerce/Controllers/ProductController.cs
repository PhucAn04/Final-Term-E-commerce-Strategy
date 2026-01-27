using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly ILogger<HomeController> _logger;

        public ProductController(ApplicationDbContext _db, ILogger<HomeController> logger)
        {
            db = _db;
            _logger = logger;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.TDanhMucSps.AsNoTracking().OrderBy(X => X.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstsanpham, pageNumber, pageSize);

            return View(lst);
        }

        public IActionResult ProductDetail(string maSp)
        {
            var sanPham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSp);
            var anhSanPham = db.TAnhSps.Where(x => x.MaSp == maSp).ToList();
            var HomeProductDetailViewModel = new HomeProductDetailViewModel
            {
                danhMucSp = sanPham,
                anhSps = anhSanPham
            };
            return View(HomeProductDetailViewModel);
        }

        public IActionResult Search(int? page, string? query)
        {
            int pageSize = 20;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var hangHoas = db.TDanhMucSps.AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                hangHoas = hangHoas.Where(p => p.TenSp.Contains(query));
            }

            var lst = hangHoas.AsNoTracking().OrderBy(x => x.TenSp)
                              .ToPagedList(pageNumber, pageSize);

            return View("Search", lst);
        }
    }
}

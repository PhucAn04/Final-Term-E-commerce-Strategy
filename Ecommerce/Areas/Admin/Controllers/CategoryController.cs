using Ecommerce.Controllers;
using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("admin/category")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly ILogger<HomeController> _logger;

        public CategoryController(ApplicationDbContext _db, ILogger<HomeController> logger)
        {
            db = _db;
            _logger = logger;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstloai = db.TLoaiSps.AsNoTracking().OrderBy(X => X.Loai);
            PagedList<TLoaiSp> lst = new PagedList<TLoaiSp>(lstloai, pageNumber, pageSize);

            return View(lst);
        }

        [Route("create")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.TLoaiSps = new SelectList(db.TLoaiSps, "MaLoai", "Loai");
            return View();
        }

        [Route("create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TDanhMucSp product)
        {
            ViewBag.TLoaiSps = new SelectList(db.TLoaiSps, "MaLoai", "Loai", product.MaLoai);

            if (ModelState.IsValid)
            {
                var LoaiSp = await db.TLoaiSps.FirstOrDefaultAsync(x => x.Loai == product.MaLoai);
                if (LoaiSp != null)
                {
                    ModelState.AddModelError("LoaiSp", "Tên sản phẩm đã tồn tại.");
                    return View(product);
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ. Vui lòng kiểm tra lại.";
                List<string> errors = new List<string>();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
            }
            return View(product);
        }
    }
}

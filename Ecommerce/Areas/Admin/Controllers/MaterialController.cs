using Ecommerce.Controllers;
using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("admin/material")]
    public class MaterialController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly ILogger<HomeController> _logger;

        public MaterialController(ApplicationDbContext _db, ILogger<HomeController> logger)
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
            var lstcl = db.TChatLieus.AsNoTracking().OrderBy(X => X.MaChatLieu);
            PagedList<TChatLieu> lst = new PagedList<TChatLieu>(lstcl, pageNumber, pageSize);

            return View(lst);
        }
    }
}

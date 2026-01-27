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
    [Area("admin")]
    [Route("admin")]
    [Route("admin/user")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly ILogger<UserController> _logger;
        public UserController(ApplicationDbContext _db, ILogger<UserController> logger)
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
            var lstuser = db.Users
                .AsNoTracking()
                .OrderBy(x => x.UserName)
                .Select(x => new UserModel
                {
                    UserName = x.UserName,
                    Email = x.Email,
                    Address = x.Address,
                    PhoneNumer = x.PhoneNumber,
                });
            var lst = new PagedList<UserModel>(lstuser, pageNumber, pageSize);

            return View(lst);
        }
    }
}

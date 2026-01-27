using Ecommerce.Models;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult RegisterAdmin()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAdmin(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }
                await _userManager.AddToRoleAsync(user, "Admin");
                TempData["Success"] = "Tạo tài khoản Admin thành công.";
                return RedirectToAction("Index", "Home");
            }

            foreach (var err in result.Errors)
                ModelState.AddModelError(string.Empty, err.Description);

            return View(model);
        }

        // =================== LOGIN (GET) ===================
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        // =================== LOGIN (POST) ===================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Tìm user theo username hoặc email
            var user = await _userManager.FindByNameAsync(model.UserName)
                       ?? await _userManager.FindByEmailAsync(model.UserName);

            if (user == null)
            {
                ModelState.AddModelError("", "Tài khoản không tồn tại.");
                return View(model);
            }

            // Kiểm tra xác nhận email nếu hệ thống yêu cầu
            if (_userManager.Options.SignIn.RequireConfirmedAccount && !user.EmailConfirmed)
            {
                ModelState.AddModelError("", "Vui lòng xác nhận email trước khi đăng nhập.");
                return View(model);
            }

            // Đăng nhập
            var result = await _signInManager.PasswordSignInAsync(
                user.UserName,      // Username
                model.Password,     // Password
                isPersistent: false,
                lockoutOnFailure: false
            );

            if (result.Succeeded)
            {
                // Nếu có ReturnUrl thì chuyển về trang cũ
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);

                // Nếu là admin → vào trang admin
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                    return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });

                // Mặc định → về trang chủ
                return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
                ModelState.AddModelError("", "Tài khoản của bạn đã bị khóa.");
            else
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");

            return View(model);
        }

        // ================= REGISTER =================
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        // ================= LOGOUT =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        // ================= ACCESS DENIED =================
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

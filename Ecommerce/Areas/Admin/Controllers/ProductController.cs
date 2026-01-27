using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using X.PagedList;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("admin/product")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly ApplicationDbContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ApplicationDbContext context, ApplicationDbContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            db = context;
            _dataContext = dataContext;
            _webHostEnvironment = webHostEnvironment;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index(int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.TDanhMucSps.AsNoTracking().OrderBy(X => X.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstsanpham, pageNumber, pageSize);

            return View(lst);
        }

        [Route("create")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.THangSxes = new SelectList(db.THangSxes, "MaHangSx", "HangSx");
            ViewBag.TQuocGias = new SelectList(db.TQuocGias, "MaNuoc", "TenNuoc");
            ViewBag.TLoaiDts = new SelectList(db.TLoaiDts, "MaDt", "TenLoai");
            ViewBag.TChatLieus = new SelectList(db.TChatLieus, "MaChatLieu", "ChatLieu");
            ViewBag.TLoaiSps = new SelectList(db.TLoaiSps, "MaLoai", "Loai");
            return View();
        }

        [Route("create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TDanhMucSp product)
        {
            ViewBag.THangSxes = new SelectList(db.THangSxes, "MaHangSx", "HangSx", product.MaHangSx);
            ViewBag.TQuocGias = new SelectList(db.TQuocGias, "MaNuoc", "TenNuoc", product.MaNuocSx);
            ViewBag.TLoaiDts = new SelectList(db.TLoaiDts, "MaDt", "TenLoai", product.MaDt);
            ViewBag.TChatLieus = new SelectList(db.TChatLieus, "MaChatLieu", "ChatLieu", product.MaChatLieu);
            ViewBag.TLoaiSps = new SelectList(db.TLoaiSps, "MaLoai", "Loai", product.MaLoai);

            if (ModelState.IsValid)
            {
                var TenSp = await db.TDanhMucSps.FirstOrDefaultAsync(x => x.TenSp == product.TenSp);
                if (TenSp != null)
                {
                    ModelState.AddModelError("TenSp", "Tên sản phẩm đã tồn tại.");
                    return View(product);
                }
                if (product.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "ProductsImages");
                    string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + product.ImageUpload.FileName.Replace(" ", "-");
                    string filePath = Path.Combine(uploadsDir, fileName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await product.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                    product.AnhDaiDien = fileName;
                }
                db.TDanhMucSps.Add(product);
                await db.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm sản phẩm thành công.";
                return RedirectToAction("Index");
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

        [Route("edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(string maSanPham)
        {
            var product = await db.TDanhMucSps.FindAsync(maSanPham);
            if (product == null)
            {
                return NotFound();
            }

            LoadDropdownLists(product);
            return View(product);
        }

        [Route("edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string maSanPham, TDanhMucSp product)
        {
            LoadDropdownLists(product);

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ. Vui lòng kiểm tra lại.";
                return View(product);
            }

            var tenSpTrung = await db.TDanhMucSps
                .FirstOrDefaultAsync(x => x.TenSp == product.TenSp && x.MaSp != maSanPham);
            if (tenSpTrung != null)
            {
                ModelState.AddModelError("TenSp", "Tên sản phẩm đã tồn tại.");
                return View(product);
            }

            var existedProduct = await db.TDanhMucSps.FindAsync(maSanPham);
            if (existedProduct == null)
            {
                return NotFound();
            }

            existedProduct.TenSp = product.TenSp;
            existedProduct.MaChatLieu = product.MaChatLieu;
            existedProduct.NganLapTop = product.NganLapTop;
            existedProduct.Model = product.Model;
            existedProduct.CanNang = product.CanNang;
            existedProduct.DoNoi = product.DoNoi;
            existedProduct.MaHangSx = product.MaHangSx;
            existedProduct.MaNuocSx = product.MaNuocSx;
            existedProduct.Website = product.Website;
            existedProduct.ThoiGianBaoHanh = product.ThoiGianBaoHanh;
            existedProduct.GioiThieuSp = product.GioiThieuSp;
            existedProduct.ChietKhau = product.ChietKhau;
            existedProduct.MaLoai = product.MaLoai;
            existedProduct.MaDt = product.MaDt;
            existedProduct.GiaNhoNhat = product.GiaNhoNhat;
            existedProduct.GiaLonNhat = product.GiaLonNhat;

            if (product.ImageUpload != null)
            {
                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "ProductsImages");
                if (!Directory.Exists(uploadsDir))
                {
                    Directory.CreateDirectory(uploadsDir);
                }

                string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + product.ImageUpload.FileName.Replace(" ", "-");
                string filePath = Path.Combine(uploadsDir, fileName);

                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    await product.ImageUpload.CopyToAsync(fs);
                }

                existedProduct.AnhDaiDien = fileName;
            }

            try
            {
                db.TDanhMucSps.Update(existedProduct);
                await db.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật sản phẩm thành công.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật sản phẩm: " + ex.Message;
                return View(product);
            }
        }

        private void LoadDropdownLists(TDanhMucSp product)
        {
            ViewBag.THangSxes = new SelectList(db.THangSxes, "MaHangSx", "HangSx", product.MaHangSx);
            ViewBag.TQuocGias = new SelectList(db.TQuocGias, "MaNuoc", "TenNuoc", product.MaNuocSx);
            ViewBag.TLoaiDts = new SelectList(db.TLoaiDts, "MaDt", "TenLoai", product.MaDt);
            ViewBag.TChatLieus = new SelectList(db.TChatLieus, "MaChatLieu", "ChatLieu", product.MaChatLieu);
            ViewBag.TLoaiSps = new SelectList(db.TLoaiSps, "MaLoai", "Loai", product.MaLoai);
        }

        [Route("delete")]
        [HttpGet]
        public async Task<IActionResult> Delete(string maSanPham)
        {
            TDanhMucSp product = await db.TDanhMucSps.FindAsync(maSanPham);

            if (product == null)
            {
                return NotFound();
            }

            if (product.AnhDaiDien != null)
            {

                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "ProductsImages");
                string oldfilePath = Path.Combine(uploadsDir, product.AnhDaiDien);
                try
                {
                    if (System.IO.File.Exists(oldfilePath))
                    {
                        System.IO.File.Delete(oldfilePath);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while deleting the product image.");
                }
            }
            db.TDanhMucSps.Remove(product);
            await db.SaveChangesAsync();
            TempData["SuccessMessage"] = "Xóa sản phẩm thành công.";
            return RedirectToAction("Index");
        }

        [Route("detail")]
        [HttpGet]
        public async Task<IActionResult> Detail(string maSanPham)
        {
            if (string.IsNullOrWhiteSpace(maSanPham))
            {
                return BadRequest("Mã sản phẩm không hợp lệ.");
            }

            var product = await db.TDanhMucSps
                .Include(p => p.MaChatLieuNavigation)
                .Include(p => p.MaHangSxNavigation)
                .Include(p => p.MaNuocSxNavigation)
                .Include(p => p.MaDtNavigation)
                .Include(p => p.MaLoaiNavigation)
                .FirstOrDefaultAsync(p => p.MaSp == maSanPham);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using X.PagedList;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("admin/order")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;

            var orders = _context.THoaDonBans
                .Include(o => o.MaTrangThaiNavigation)
                .AsNoTracking()
                .OrderByDescending(o => o.NgayDat);

            var lst = new PagedList<THoaDonBan>(orders, pageNumber, pageSize);
            return View(lst);
        }

        // Chi tiết đơn hàng
        [Route("detail/{id}")]
        public IActionResult Detail(int id)
        {
            var order = _context.THoaDonBans
                .Include(o => o.MaTrangThaiNavigation)
                .Include(o => o.TChiTietHdbs)
                    .ThenInclude(ct => ct.MaSpNavigation)
                .FirstOrDefault(o => o.MaHoaDon == id);

            if (order == null)
                return NotFound();

            var vm = new OrderHistoryViewModel
            {
                MaHoaDon = order.MaHoaDon,
                NgayDat = order.NgayDat,
                HoTen = order.HoTen,
                username = order.UserName,
                DiaChi = order.DiaChi,
                PhoneNumber = order.PhoneNumber,
                CachThanhToan = order.CachThanhToan,
                CachVanChuyen = order.CachVanChuyen,
                TrangThai = order.MaTrangThaiNavigation?.TenTrangThai ?? "",
                TongTienHd = order.TongTienHd,
                GhiChu = order.GhiChu
            };

            vm.Items = order.TChiTietHdbs.Select(ct => new OrderItemViewModel
            {
                MaSp = ct.MaSp,
                TenSp = ct.MaSpNavigation?.TenSp ?? "",
                DonGiaBan = ct.DonGiaBan,
                SoLuongBan = ct.SoLuongBan,
                GiamGia = ct.GiamGia
            }).ToList();

            return View(vm);
        }
    }
}
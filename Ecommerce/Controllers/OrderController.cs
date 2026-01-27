using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

[Authorize]
public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> History()
    {
        var user = await _userManager.GetUserAsync(User);
        var orders = await _context.THoaDonBans
            .Where(o => o.UserName == user.UserName)
            .Include(o => o.TChiTietHdbs)
                .ThenInclude(c => c.MaSpNavigation)
            .Include(o => o.MaTrangThaiNavigation)
            .OrderByDescending(o => o.NgayDat)
            .Select(o => new OrderHistoryViewModel
            {
                MaHoaDon = o.MaHoaDon,
                NgayDat = o.NgayDat,
                HoTen = o.HoTen,
                DiaChi = o.DiaChi,
                PhoneNumber = o.PhoneNumber,
                CachThanhToan = o.CachThanhToan,
                CachVanChuyen = o.CachVanChuyen,
                TongTienHd = o.TongTienHd,
                TrangThai = o.MaTrangThaiNavigation.TenTrangThai,
                GhiChu = o.GhiChu,
                Items = o.TChiTietHdbs.Select(c => new OrderItemViewModel
                {
                    MaSp = c.MaSp,
                    TenSp = c.MaSpNavigation.TenSp,
                    DonGiaBan = c.DonGiaBan,
                    SoLuongBan = c.SoLuongBan,
                    GiamGia = c.GiamGia
                }).ToList()
            })
            .ToListAsync();

        return View(orders);
    }
}

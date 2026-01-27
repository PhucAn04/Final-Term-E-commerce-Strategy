using System;
using System.Collections.Generic;

namespace Ecommerce.ViewModels
{
    public class OrderHistoryViewModel
    {
        public int MaHoaDon { get; set; }
        public DateTime NgayDat { get; set; }
        public string NgayDatFormatted => NgayDat.ToString("dd/MM/yyyy");

        public string HoTen { get; set; }
        public string username { get; set; }
        public string DiaChi { get; set; }
        public string PhoneNumber { get; set; }
        public string CachThanhToan { get; set; }
        public string CachVanChuyen { get; set; }

        public decimal? TongTienHd { get; set; }
        public string TongTienFormatted => TongTienHd?.ToString("C0");

        public string TrangThai { get; set; }
        public string? GhiChu { get; set; }

        public List<OrderItemViewModel> Items { get; set; } = new();
    }

    public class OrderItemViewModel
    {
        public string MaSp { get; set; }
        public string TenSp { get; set; }
        public decimal? DonGiaBan { get; set; }
        public string DonGiaFormatted => DonGiaBan?.ToString("C0");
        public int? SoLuongBan { get; set; }
        public double? GiamGia { get; set; }
        public decimal? ThanhTien => DonGiaBan.HasValue && SoLuongBan.HasValue
            ? DonGiaBan.Value * SoLuongBan.Value * (decimal)(1 - (GiamGia ?? 0) / 100)
            : null;
        public string ThanhTienFormatted => ThanhTien?.ToString("C0");
    }
}

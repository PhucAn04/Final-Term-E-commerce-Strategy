using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models;

[Table("tHoaDonBan")]
public partial class THoaDonBan
{
    [Key]
    public int MaHoaDon { get; set; }

    public string UserId { get; set; }

    [StringLength(256)]
    public string? UserName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime NgayDat { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayCan { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayGiao { get; set; }

    [StringLength(50)]
    public string? HoTen { get; set; }

    [StringLength(60)]
    public string DiaChi { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    [StringLength(50)]
    public string CachThanhToan { get; set; } = null!;

    [StringLength(50)]
    public string CachVanChuyen { get; set; } = null!;

    public double PhiVanChuyen { get; set; }

    public int MaTrangThai { get; set; }

    [Column("TongTienHD", TypeName = "money")]
    public decimal? TongTienHd { get; set; }

    [Column("GiamGiaHD")]
    public double? GiamGiaHd { get; set; }

    [StringLength(100)]
    public string? GhiChu { get; set; }

    [ForeignKey("MaTrangThai")]
    [InverseProperty("THoaDonBans")]
    public virtual TTrangThai MaTrangThaiNavigation { get; set; } = null!;

    [InverseProperty("MaHoaDonNavigation")]
    public virtual ICollection<TChiTietHdb> TChiTietHdbs { get; set; } = new List<TChiTietHdb>();

    [ForeignKey("UserId")]
    public virtual ApplicationUser UserNameNavigation { get; set; }
}

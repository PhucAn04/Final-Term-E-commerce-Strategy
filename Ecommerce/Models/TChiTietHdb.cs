using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models;

[Table("tChiTietHDB")]
public partial class TChiTietHdb
{
    [Key]
    [Column("MaChiTietHDB")]
    public int MaChiTietHdb { get; set; }

    public int MaHoaDon { get; set; }

    [Column("MaSP")]
    [StringLength(25)]
    [Unicode(false)]
    public string MaSp { get; set; }

    public string UserId { get; set; }

    [StringLength(256)]
    public string? UserName { get; set; }

    [Column(TypeName = "money")]
    public decimal? DonGiaBan { get; set; }

    public int? SoLuongBan { get; set; }

    public double? GiamGia { get; set; }

    [StringLength(100)]
    public string? GhiChu { get; set; }

    [ForeignKey("MaHoaDon")]
    [InverseProperty("TChiTietHdbs")]
    public virtual THoaDonBan MaHoaDonNavigation { get; set; } = null!;

    [ForeignKey("MaSp")]
    [InverseProperty("TChiTietHdbs")]
    public virtual TDanhMucSp MaSpNavigation { get; set; } = null!;

    [ForeignKey("UserId")]
    public virtual ApplicationUser UserNameNavigation { get; set; }
}

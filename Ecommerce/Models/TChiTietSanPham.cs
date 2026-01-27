using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Models;

[Table("tChiTietSanPham")]
public partial class TChiTietSanPham
{
    [Key]
    [Column("MaChiTietSP")]
    [StringLength(25)]
    [Unicode(false)]
    public string MaChiTietSp { get; set; } = null!;

    [Column("MaSP")]
    [StringLength(25)]
    [Unicode(false)]
    public string? MaSp { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string? MaKichThuoc { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string? MaMauSac { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? AnhDaiDien { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Video { get; set; }

    public double? DonGiaBan { get; set; }

    public double? GiamGia { get; set; }

    [Column("SLTon")]
    public int? Slton { get; set; }

    [ForeignKey("MaKichThuoc")]
    [InverseProperty("TChiTietSanPhams")]
    public virtual TKichThuoc? MaKichThuocNavigation { get; set; }

    [ForeignKey("MaMauSac")]
    [InverseProperty("TChiTietSanPhams")]
    public virtual TMauSac? MaMauSacNavigation { get; set; }

    [ForeignKey("MaSp")]
    [InverseProperty("TChiTietSanPhams")]
    public virtual TDanhMucSp? MaSpNavigation { get; set; }

    [InverseProperty("MaChiTietSpNavigation")]
    public virtual ICollection<TAnhChiTietSp> TAnhChiTietSps { get; set; } = new List<TAnhChiTietSp>();
}

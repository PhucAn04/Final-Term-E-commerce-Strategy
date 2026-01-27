using Ecommerce.Repositories.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models;

[Table("tDanhMucSP")]
public partial class TDanhMucSp
{
    [Key]
    [Column("MaSP")]
    [StringLength(25)]
    [Unicode(false)]
    public string MaSp { get; set; } = null!;

    [Column("TenSP")]
    [StringLength(150)]
    public string? TenSp { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string? MaChatLieu { get; set; }

    [StringLength(55)]
    public string? NganLapTop { get; set; }

    [StringLength(55)]
    public string? Model { get; set; }

    public double? CanNang { get; set; }

    public double? DoNoi { get; set; }

    [Column("MaHangSX")]
    [StringLength(25)]
    [Unicode(false)]
    public string? MaHangSx { get; set; }

    [Column("MaNuocSX")]
    [StringLength(25)]
    [Unicode(false)]
    public string? MaNuocSx { get; set; }

    [StringLength(155)]
    [Unicode(false)]
    public string? Website { get; set; }

    public double? ThoiGianBaoHanh { get; set; }

    [Column("GioiThieuSP")]
    [StringLength(255)]
    public string? GioiThieuSp { get; set; }

    public double? ChietKhau { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string? MaLoai { get; set; }

    [Column("MaDT")]
    [StringLength(25)]
    [Unicode(false)]
    public string? MaDt { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? AnhDaiDien { get; set; }

    [Column(TypeName = "money")]
    public decimal? GiaNhoNhat { get; set; }

    [Column(TypeName = "money")]
    public decimal? GiaLonNhat { get; set; }

    [NotMapped]
    [FileExtension]
    public IFormFile? ImageUpload { get; set; }

    [ForeignKey("MaChatLieu")]
    [InverseProperty("TDanhMucSps")]
    public virtual TChatLieu? MaChatLieuNavigation { get; set; }

    [ForeignKey("MaDt")]
    [InverseProperty("TDanhMucSps")]
    public virtual TLoaiDt? MaDtNavigation { get; set; }

    [ForeignKey("MaHangSx")]
    [InverseProperty("TDanhMucSps")]
    public virtual THangSx? MaHangSxNavigation { get; set; }

    [ForeignKey("MaLoai")]
    [InverseProperty("TDanhMucSps")]
    public virtual TLoaiSp? MaLoaiNavigation { get; set; }

    [ForeignKey("MaNuocSx")]
    [InverseProperty("TDanhMucSps")]
    public virtual TQuocGia? MaNuocSxNavigation { get; set; }

    [InverseProperty("MaSpNavigation")]
    public virtual ICollection<TAnhSp> TAnhSps { get; set; } = new List<TAnhSp>();

    [InverseProperty("MaSpNavigation")]
    public virtual ICollection<TChiTietHdb> TChiTietHdbs { get; set; } = new List<TChiTietHdb>();

    [InverseProperty("MaSpNavigation")]
    public virtual ICollection<TChiTietSanPham> TChiTietSanPhams { get; set; } = new List<TChiTietSanPham>();
}

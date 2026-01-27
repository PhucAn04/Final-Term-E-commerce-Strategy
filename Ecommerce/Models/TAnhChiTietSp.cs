using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Models;

[PrimaryKey("MaChiTietSp", "TenFileAnh")]
[Table("tAnhChiTietSP")]
public partial class TAnhChiTietSp
{
    [Key]
    [Column("MaChiTietSP")]
    [StringLength(25)]
    [Unicode(false)]
    public string MaChiTietSp { get; set; } = null!;

    [Key]
    [StringLength(100)]
    [Unicode(false)]
    public string TenFileAnh { get; set; } = null!;

    public short? ViTri { get; set; }

    [ForeignKey("MaChiTietSp")]
    [InverseProperty("TAnhChiTietSps")]
    public virtual TChiTietSanPham MaChiTietSpNavigation { get; set; } = null!;
}

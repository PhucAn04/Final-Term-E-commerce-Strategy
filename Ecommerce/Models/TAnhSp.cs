using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Models;

[PrimaryKey("MaSp", "TenFileAnh")]
[Table("tAnhSP")]
public partial class TAnhSp
{
    [Key]
    [Column("MaSP")]
    [StringLength(25)]
    [Unicode(false)]
    public string MaSp { get; set; } = null!;

    [Key]
    [StringLength(100)]
    [Unicode(false)]
    public string TenFileAnh { get; set; } = null!;

    public short? ViTri { get; set; }

    [ForeignKey("MaSp")]
    [InverseProperty("TAnhSps")]
    public virtual TDanhMucSp MaSpNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Models;

[Table("tHangSX")]
public partial class THangSx
{
    [Key]
    [Column("MaHangSX")]
    [StringLength(25)]
    [Unicode(false)]
    public string MaHangSx { get; set; } = null!;

    [Column("HangSX")]
    [StringLength(100)]
    public string? HangSx { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string? MaNuocThuongHieu { get; set; }

    [InverseProperty("MaHangSxNavigation")]
    public virtual ICollection<TDanhMucSp> TDanhMucSps { get; set; } = new List<TDanhMucSp>();
}

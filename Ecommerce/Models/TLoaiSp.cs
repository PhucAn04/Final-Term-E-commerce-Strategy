using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Models;

[Table("tLoaiSP")]
public partial class TLoaiSp
{
    [Key]
    [StringLength(25)]
    [Unicode(false)]
    public string MaLoai { get; set; } = null!;

    [StringLength(100)]
    public string? Loai { get; set; }

    [InverseProperty("MaLoaiNavigation")]
    public virtual ICollection<TDanhMucSp> TDanhMucSps { get; set; } = new List<TDanhMucSp>();
}

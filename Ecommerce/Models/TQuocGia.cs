using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Models;

[Table("tQuocGia")]
public partial class TQuocGia
{
    [Key]
    [StringLength(25)]
    [Unicode(false)]
    public string MaNuoc { get; set; } = null!;

    [StringLength(100)]
    public string? TenNuoc { get; set; }

    [InverseProperty("MaNuocSxNavigation")]
    public virtual ICollection<TDanhMucSp> TDanhMucSps { get; set; } = new List<TDanhMucSp>();
}

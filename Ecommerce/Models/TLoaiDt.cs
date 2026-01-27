using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Models;

[Table("tLoaiDT")]
public partial class TLoaiDt
{
    [Key]
    [Column("MaDT")]
    [StringLength(25)]
    [Unicode(false)]
    public string MaDt { get; set; } = null!;

    [StringLength(100)]
    public string? TenLoai { get; set; }

    [InverseProperty("MaDtNavigation")]
    public virtual ICollection<TDanhMucSp> TDanhMucSps { get; set; } = new List<TDanhMucSp>();
}

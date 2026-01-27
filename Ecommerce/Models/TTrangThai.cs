using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Models;

[Table("tTrangThai")]
public partial class TTrangThai
{
    [Key]
    public int MaTrangThai { get; set; }

    [StringLength(50)]
    public string TenTrangThai { get; set; } = null!;

    [StringLength(500)]
    public string? MoTa { get; set; }

    [InverseProperty("MaTrangThaiNavigation")]
    public virtual ICollection<THoaDonBan> THoaDonBans { get; set; } = new List<THoaDonBan>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Models;

[Table("tMauSac")]
public partial class TMauSac
{
    [Key]
    [StringLength(25)]
    [Unicode(false)]
    public string MaMauSac { get; set; } = null!;

    [StringLength(100)]
    public string? TenMauSac { get; set; }

    [InverseProperty("MaMauSacNavigation")]
    public virtual ICollection<TChiTietSanPham> TChiTietSanPhams { get; set; } = new List<TChiTietSanPham>();
}

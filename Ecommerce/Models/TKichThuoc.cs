using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Models;

[Table("tKichThuoc")]
public partial class TKichThuoc
{
    [Key]
    [StringLength(25)]
    [Unicode(false)]
    public string MaKichThuoc { get; set; } = null!;

    [StringLength(150)]
    public string? KichThuoc { get; set; }

    [InverseProperty("MaKichThuocNavigation")]
    public virtual ICollection<TChiTietSanPham> TChiTietSanPhams { get; set; } = new List<TChiTietSanPham>();
}

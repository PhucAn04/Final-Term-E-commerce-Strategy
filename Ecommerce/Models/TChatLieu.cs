using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Models;

[Table("tChatLieu")]
public partial class TChatLieu
{
    [Key]
    [StringLength(25)]
    [Unicode(false)]
    public string MaChatLieu { get; set; } = null!;

    [StringLength(150)]
    public string? ChatLieu { get; set; }

    [InverseProperty("MaChatLieuNavigation")]
    public virtual ICollection<TDanhMucSp> TDanhMucSps { get; set; } = new List<TDanhMucSp>();
}

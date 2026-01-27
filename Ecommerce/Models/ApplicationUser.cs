using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

public class ApplicationUser : IdentityUser
{
    [Required(ErrorMessage = "Làm ơn nhập Username")]
    public override string UserName { get; set; }

    [Required(ErrorMessage = "Làm ơn nhập Email")]
    [EmailAddress]
    public override string Email { get; set; }

    [Required]
    public bool Gender { get; set; }

    [Required(ErrorMessage = "Làm ơn nhập Date Of Birth")]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Làm ơn nhập Address")]
    public string? Address { get; set; }

    [Required(ErrorMessage = "Làm ơn nhập Phone Number")]
    public override string PhoneNumber { get; set; }

    public string? Avatar { get; set; }

    public bool Enforceable { get; set; }

    public int CustomerRole { get; set; }

    public string? RandomKey { get; set; }

    public virtual ICollection<THoaDonBan> THoaDonBans { get; set; } = new List<THoaDonBan>();
    public virtual ICollection<TChiTietHdb> TChiTietHdbs { get; set; } = new List<TChiTietHdb>();
}

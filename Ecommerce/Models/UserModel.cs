using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class UserModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Làm ơn nhập Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Làm ơn nhập Email"), EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Làm ơn nhập Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Làm ơn nhập Phone Number")]
        public string PhoneNumer { get; set; }

        [DataType(DataType.Password), Required(ErrorMessage = "Làm ơn nhập Password")]
        public string Password { get; set; }
    }
}

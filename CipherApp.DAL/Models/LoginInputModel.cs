using System.ComponentModel.DataAnnotations;

namespace CipherApp.DAL.Models
{
    public class LoginInputModel
    {
        [Required, Display(Name = "Username")]
        public string Username { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Password")]
        public string Password { get; set; }
    }
}

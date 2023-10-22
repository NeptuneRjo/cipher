using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherApp.DAL.Models
{
    public class RegisterInputModel
    {
        [Required, Display(Name = "Username")]
        public string Username { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Password one")]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Password two")]
        public string Password2 { get; set; }
    }
}

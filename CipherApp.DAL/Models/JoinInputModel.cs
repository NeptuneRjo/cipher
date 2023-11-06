using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherApp.DAL.Models
{
    public class JoinInputModel
    {
        [Required]
        public string UID { get; set; }
    }
}

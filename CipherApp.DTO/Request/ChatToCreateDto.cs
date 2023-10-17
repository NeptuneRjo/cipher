using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherApp.DTO.Request
{
    public class ChatToCreateDto
    {
        public string? Name { get; set; }
        public int? OwnerId { get; set; }
    }
}

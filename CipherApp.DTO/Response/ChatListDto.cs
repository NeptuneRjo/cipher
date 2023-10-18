using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherApp.DTO.Response
{
    public class ChatListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public DateTime LastMessage { get; set; }

        public ICollection<UserDto> Users { get; set; }
    }
}

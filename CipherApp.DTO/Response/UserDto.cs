

namespace CipherApp.DTO.Response
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public ICollection<ChatDto> Chats { get; set; }
     
        public ICollection<MessageDto> Messages { get; set; }
    }
}

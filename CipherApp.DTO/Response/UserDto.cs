

namespace CipherApp.DTO.Response
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<ChatDto> Chats { get; set; }

        public ICollection<MessageDto> Messages { get; set; }
    }
}


namespace CipherApp.DTO.Response
{
    public class ChatDto
    {
        public int Id { get; set; }
        public string UID { get; set; }
        public ICollection<UserDto> Users { get; set; }
        public ICollection<MessageDto> Messages { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime LastMessage { get; set; }
    }
}

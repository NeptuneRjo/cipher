
namespace CipherApp.DTO.Response
{
    public class ChatDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public DateTime LastMessage { get; set; }

        public ICollection<UserDto> Users { get; set; }

        public ICollection<MessageDto> Messages { get; set; }
    }
}

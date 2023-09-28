
namespace CipherApp.DTO.Response
{
    public class MessageDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserDto User { get; set; }
        public ChatDto Chat { get; set; }
    }
}

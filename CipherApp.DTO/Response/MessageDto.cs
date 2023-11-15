
namespace CipherApp.DTO.Response
{
    public class MessageDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }

        public UserDto User { get; set; }
    }
}

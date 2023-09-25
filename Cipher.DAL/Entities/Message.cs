
namespace Cipher.DAL.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public int ChatId { get; set; }
        public string Username { get; set; }
    }
}

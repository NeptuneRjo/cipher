namespace CipherApp.DAL.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public string UID { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Message> Messages { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime LastMessage { get; set; }

        public Chat()
        {
            CreatedAt = DateTime.Now;
            LastMessage = DateTime.Now;
        }
    }
}

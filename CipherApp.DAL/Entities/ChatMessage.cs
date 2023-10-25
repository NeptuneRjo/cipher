namespace CipherApp.DAL.Entities
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public int SenderId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }

        public Chat Chat { get; set; }
        public User Sender { get; set; }
    }
}

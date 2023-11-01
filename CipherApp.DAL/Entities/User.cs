namespace CipherApp.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public ICollection<Message> Messages { get; set; }

        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}

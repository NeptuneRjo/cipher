namespace CipherApp.DAL.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int OwnerId { get; set; }
        public DateTime LastMessage { get; set; }
    }
}

namespace CipherApp.DAL.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public int ParticipantOneId { get; set; }
        public int ParticipantTwoId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime LastMessage { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}

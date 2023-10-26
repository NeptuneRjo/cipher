
namespace CipherApp.DTO.Response
{
    public class ChatDto
    {
        public int Id { get; set; }
        public int ParticipantOneId { get; set; }
        public int ParticipantTwoId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime LastMessage { get; set; }

        public ICollection<MessageDto> Messages { get; set; }
    }
}

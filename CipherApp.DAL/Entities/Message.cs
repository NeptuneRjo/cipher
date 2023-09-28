﻿namespace CipherApp.DAL.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }

        public int ChatId { get; set; }
        public Chat Chat { get; set; }

    }
}

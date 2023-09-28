﻿namespace CipherApp.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string Username { get; set; }
        public string UID { get; set; }
        public bool OAuth { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<ChatUser> ChatUsers { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
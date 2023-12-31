﻿namespace CipherApp.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Message> Messages { get; set; }

        public ICollection<Chat> Chats { get; set; }
    }
}

using CipherApp.DAL.Entities;
using CipherApp.DAL.Models;
using CipherApp.DTO.Response;

namespace CipherApp.Test
{
    using BCrypt.Net;

    public static class TestEntities
    {
        public static readonly Chat _mockChat = new()
        {
            CreatedAt = DateTime.UtcNow,
            UID = "1234ABC",
            Messages = new List<Message>(),
            Users = new List<User>(),
            LastMessage = DateTime.UtcNow,
        };

        public static readonly ChatDto _mockChatDto = new()
        {
            Id = 1,
            UID = "1234ABC",
            LastMessage = new DateTime(),
            CreatedAt = new DateTime(),
            Messages = new List<MessageDto>(),
            Users = new List<UserDto>(),
        };

        public static readonly User _mockUser = new()
        {
            Username = "test",
            Email = "test@email.com",
            Password = BCrypt.HashPassword("password"),
            Messages = new List<Message>(),
            Chats = new List<Chat>(),
        };

        public static readonly UserDto _mockUserDto = new()
        {
            Id = 1,
            Username = "testUsername",
            Email = "test@email.com",
        };

        public static readonly Message _mockMessage = new()
        {
            Id = 1,
            Content = "Hello World!",
            User = new User(),
            Chat = new Chat(),
            CreatedAt = new DateTime(),
            ChatId = 1,
            UserId = 1,
        };

        public static readonly MessageDto _mockMessageDto = new()
        {
            Id = 1,
            Content = "Hello World!",
            CreatedAt = new DateTime(),
            User = new UserDto(),
        };

        public static readonly LoginInputModel _mockLoginModel = new()
        {
            Email = "test@email.com",
            Password = "password",
        };

        public static readonly RegisterInputModel _mockRegisterModel = new()
        {
            Username = "testUsername",
            Email = "test@email.com",
            Password = "password",
            ConfirmPassword = "password"
        };
    }
}

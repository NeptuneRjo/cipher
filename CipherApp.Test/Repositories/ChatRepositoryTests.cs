using CipherApp.DAL.Data;
using CipherApp.DAL.Entities;
using CipherApp.DAL.Repositories;
using CipherApp.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CipherApp.Test.Repositories
{
    public class ChatRepositoryTests : IDisposable
    {
        private readonly DataContext _context;
        private readonly IChatRepository _repository;

        public ChatRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new DataContext(options);

            _context.Database.EnsureDeleted();

            _context.Users.Add(TestEntities._mockUser);
            _context.Chats.Add(TestEntities._mockChat);
            _context.SaveChanges();

            _repository = new ChatRepository(_context);
        }

        [Fact]
        public async Task AddUserToChat_ReturnsUpdatedChat()
        {
            var result = await _repository.AddUserToChat("test@email.com", "1234ABC");

            Assert.NotNull(result);
            Assert.Equal(result.Users.Count(), 1);
        }

        [Fact]
        public async Task CreateChatByEmail_ReturnsNewChat()
        {
            var result = await _repository
                .CreateChatByEmail(TestEntities._mockUser.Email);

            Assert.NotNull(result);
            Assert.Equal(result.Users.Count(), 1);
        }

        [Fact]
        public async Task RemoveUserFromChat_ReturnsUpdatedChat()
        {
            Chat mockChat = new()
            {
                CreatedAt = DateTime.UtcNow,
                UID = "ABC1234",
                Messages = new List<Message>(),
                Users = new List<User>()
                {
                    TestEntities._mockUser
                },
                LastMessage = DateTime.UtcNow,
            };

            _context.Chats.Add(mockChat);
            _context.SaveChanges();

            var result = await _repository.RemoveUserFromChat("test@email.com", "ABC1234");

            Assert.NotNull(result);
            Assert.Equal(result.Users.Count(), 0);
            Assert.NotEqual(result.Users.Count(), 1);
        }

        public void Dispose() => _context.Database.EnsureDeleted();
    }
}

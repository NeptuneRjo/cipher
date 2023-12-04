using CipherApp.DAL.Data;
using CipherApp.DAL.Entities;
using CipherApp.DAL.Repositories;
using CipherApp.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CipherApp.Test.Repositories
{
    public class GenericRepositoryTests : IDisposable
    {
        private readonly DataContext _context;
        private readonly IGenericRepository<Chat> _repository;

        public GenericRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new DataContext(options);

            _repository = new GenericRepository<Chat>(_context);
        }

        [Fact]
        public async Task AddEntityAsync_ReturnsAddedEntity()
        {
            var result = await _repository.AddEntityAsync(TestEntities._mockChat);

            Assert.NotNull(result);
            Assert.Equivalent(result, TestEntities._mockChat, strict: true);
        }

        [Fact]
        public async Task AddRangeAsync_ReturnsAddedEntities()
        {
            var result = await _repository.AddRangeAsync(
                new List<Chat>() 
                { 
                    TestEntities._mockChat, 
                    TestEntities._mockChat 
                });

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task DeleteAsync_DeletesEntity()
        {
            _context.Chats.Add(TestEntities._mockChat);
            _context.SaveChanges();

            await _repository.DeleteAsync(TestEntities._mockChat);

            bool entity = await _context.Chats.AnyAsync(e => e.UID == TestEntities._mockChat.UID);

            Assert.False(entity);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsTheUpdatedEntity()
        {
            _context.Chats.Add(TestEntities._mockChat);
            _context.SaveChanges();

            Chat chat = TestEntities._mockChat;
            chat.CreatedAt = DateTime.Now;

            var entity = await _repository.UpdateAsync(chat);

            Assert.Equivalent(entity, chat, strict: true);
        }

        [Fact]
        public async Task ExistsAsync_ReturnsTrueIfExists()
        {
            _context.Chats.Add(TestEntities._mockChat);
            _context.SaveChanges();

            var result = await _repository
                .ExistsAsync(e => e.UID == TestEntities._mockChat.UID);

            Assert.True(result);
        }

        [Fact]
        public async Task ExistsAsync_ReturnsFalseIfDoesntExist()
        {
            var result = await _repository
                .ExistsAsync(e => e.UID == TestEntities._mockChat.UID);

            Assert.False(result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllEntities()
        {
            List<Chat> chats = new()
            {
                TestEntities._mockChat,
            };

            await _context.Chats.AddRangeAsync(chats);
            _context.SaveChanges();

            var result = await _repository.GetAllAsync();

            Assert.NotEmpty(result);
            Assert.Equivalent(result, chats, strict: true);
        }

        [Fact]
        public async Task GetAllByQueryAsync_ReturnsEntities()
        {
            List<Chat> chats = new()
            {
                TestEntities._mockChat,
            };

            await _context.Chats.AddRangeAsync(chats);
            _context.SaveChanges();

            Expression<Func<Chat, object>>[] includes =
            {
                e => e.Users,
                e => e.Messages
            };

            var result = await _repository
                .GetAllByQueryAsync(
                    e => e.UID == TestEntities._mockChat.UID, includes);

            Assert.NotEmpty(result);
            Assert.Equivalent(result, chats, strict: true);
        }

        [Fact]
        public async Task GetByQueryAsync_ReturnsEntity()
        {
            await _context.AddAsync(TestEntities._mockChat);
            _context.SaveChanges();

            Expression<Func<Chat, object>>[] includes =
{
                e => e.Users,
                e => e.Messages
            };

            var result = await _repository
                .GetByQueryAsync(e => e.UID == TestEntities._mockChat.UID, includes);

            Assert.NotNull(result);
            Assert.Equivalent(result, TestEntities._mockChat, strict: true);
        }

        public void Dispose() => _context.Database.EnsureDeleted();
    }
}

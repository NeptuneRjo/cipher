using AutoMapper;
using CipherApp.BLL.Services.IServices;
using CipherApp.BLL.Services;
using CipherApp.DAL.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using NSubstitute;
using System.Linq.Expressions;
using CipherApp.DAL.Entities;
using CipherApp.DTO.Response;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Models;

namespace CipherApp.Test.Services
{
    public class ChatServiceTests
    {
        private readonly IChatService _service;

        private readonly IChatRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ChatService> _logger;

        private readonly ITestOutputHelper _output;

        private readonly Chat _mockChat = new Chat()
        {
            Id = 1,
            UID = "1234ABC",
            LastMessage = new DateTime(),
            CreatedAt = new DateTime(),
            Messages = new List<Message>(),
            Users = new List<User>(),
        };

        private readonly ChatDto _mockChatDto = new ChatDto()
        {
            Id = 1,
            UID = "1234ABC",
            LastMessage = new DateTime(),
            CreatedAt = new DateTime(),
            Messages = new List<MessageDto>(),
            Users = new List<UserDto>(),
        };

        public ChatServiceTests()
        {
            _repository = Substitute.For<IChatRepository>();
            _mapper = Substitute.For<IMapper>();
            _logger = Substitute.For<ILogger<ChatService>>();

            _service = new ChatService(_mapper, _logger, _repository);
        }

        [Fact]
        public async Task GetChatAsync_WhenSuccess_ReturnsDto()
        {
            _repository.GetByQueryAsync(
                Arg.Any<Expression<Func<Chat, bool>>>(), 
                Arg.Any<Expression<Func<Chat, object>>[]>())
                .Returns(Task.FromResult(_mockChat));

            _mapper.Map<ChatDto>(_mockChat).Returns(_mockChatDto);

            var result = await _service.GetChatAsync("test");

            Assert.NotNull(result);
            Assert.Equivalent(result, _mockChatDto, strict: true);
        }

        [Fact]
        public async Task GetChatAsync_WhenChatNotFound_ThrowsNotFoundException()
        {
            await Assert.ThrowsAsync<NotFoundException>(
                () => _service.GetChatAsync("test"));
        }

        [Fact]
        public async Task CreateChatAsync_WhenSuccess_ReturnsDto()
        {
            Chat mockChat = new()
            {
                Id = 1,
                UID = "1234ABC",
                LastMessage = new DateTime(),
                CreatedAt = new DateTime(),
                Messages = new List<Message>(),
                Users = new List<User>()
                {
                    new User()
                    {
                        Id = 1,
                        Email = "test@email.com",
                        Username = "Username",
                        Password = "password",
                        Messages = new List<Message>()
                    }
                },
            };

            ChatDto mockChatDto = new()
            {
                Id = 1,
                UID = "1234ABC",
                LastMessage = new DateTime(),
                CreatedAt = new DateTime(),
                Messages = new List<MessageDto>(),
                Users = new List<UserDto>()
                {
                    new UserDto()
                    {
                        Id = 1,
                        Email = "test@email.com",
                        Username = "username",
                    }
                }
            };

            _repository.CreateChatByEmail("test@email.com").Returns(mockChat);

            _mapper.Map<ChatDto>(Arg.Any<Chat>()).Returns(mockChatDto);

            var result = await _service.CreateChatAsync("test@email.com");

            Assert.NotNull(result);
            Assert.Equivalent(result, mockChatDto, strict: true);
        }

        [Fact]
        public async Task GetChatsByUserAsync_WhenSuccess_ReturnsDtos()
        {
            _repository.GetAllByQueryAsync(
                Arg.Any<Expression<Func<Chat, bool>>>(),
                Arg.Any<Expression<Func<Chat, object>>[]>())
                .Returns(new List<Chat>() { _mockChat, _mockChat });

            _mapper.Map<ICollection<ChatDto>>(Arg.Any<ICollection<Chat>>())
                .Returns(new List<ChatDto>() { _mockChatDto, _mockChatDto });

            var result = await _service.GetChatsByUserAsync("test@email.com");

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task AddUserAsync_WhenSuccess_ReturnsDto()
        {
            _repository.AddUserToChat("test@email.com", "123ABC").Returns(_mockChat);

            _mapper.Map<ChatDto>(Arg.Any<Chat>()).Returns(_mockChatDto);

            var result = await _service.AddUserAsync("test@email.com", "123ABC");

            Assert.NotNull(result);
            Assert.Equivalent(result, _mockChatDto, strict: true);
        }

        [Fact]
        public async Task ChatExistsAsync_WhenSuccess_ReturnsTrue()
        {
            _repository.ExistsAsync(Arg.Any<Expression<Func<Chat, bool>>>()).Returns(true);

            var result = await _service.ChatExistsAsync("123ABC");

            Assert.True(result);
        }
    }
}

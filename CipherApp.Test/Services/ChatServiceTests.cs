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
using CipherApp.DTO.Request;

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
            Name = "Test",
            OwnerId = 1,
            LastMessage = new DateTime(),
            ChatUsers = new List<ChatUser>()
            {
                new ChatUser()
                    {
                        User = new User()
                        {
                            Username = "Test",
                        }
                    }
                },
            Messages = new List<Message>(),
        };

        private readonly ChatDto _mockChatDto = new ChatDto()
        {
            Id = 1,
            Name = "Test",
            OwnerId = 1,
            LastMessage = new DateTime(),
            Users = new List<UserDto>(),
            Messages = new List<MessageDto>(),
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
                Arg.Any<Expression<Func<Chat, object>>[]>()
                )
                .Returns(Task.FromResult(_mockChat));

            _mapper.Map<ChatDto>(_mockChat).Returns(_mockChatDto);

            var result = await _service.GetChatAsync(1, "Test");

            Assert.NotNull(result);
            Assert.Equivalent(result, _mockChatDto, strict: true);
        }

        [Fact]
        public async Task GetChatAsync_WhenChatNotFound_ThrowsNotFoundException()
        {
            await Assert.ThrowsAsync<NotFoundException>(
                () => _service.GetChatAsync(1, "test")
                );
        }

        [Fact]
        public async Task GetChatAsync_WhenUserNotInChat_ThrowsUnauthorizedAccessException()
        {
            Chat chat = new()
            {
                Id = 1,
                Name = "Test",
                OwnerId = 1,
                LastMessage = new DateTime(),
                ChatUsers = new List<ChatUser>(),
                Messages = new List<Message>(),
            };

            _repository.GetByQueryAsync(
                Arg.Any<Expression<Func<Chat, bool>>>(),
                Arg.Any<Expression<Func<Chat, object>>[]>()
                )
                .Returns(Task.FromResult(chat));

            await Assert.ThrowsAsync<UnauthorizedAccessException>(
                () => _service.GetChatAsync(1, "test")
                );
        }

        [Fact]
        public async Task CreateChatAsync_WhenSuccess_ReturnsDto()
        {
            _repository.AddEntityAsync(Arg.Any<Chat>()).Returns(Task.FromResult(_mockChat));

            _mapper.Map<ChatDto>(Arg.Any<Chat>()).Returns(_mockChatDto);

            var result = await _service.CreateChatAsync(new ChatToCreateDto(), 1);

            Assert.NotNull(result);
            Assert.Equivalent(result, _mockChatDto, strict: true);
        }
    }
}

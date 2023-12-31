﻿using AutoMapper;
using CipherApp.BLL.Services;
using CipherApp.BLL.Services.IServices;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Entities;
using CipherApp.DAL.Repositories.IRepositories;
using CipherApp.DTO.Response;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Linq.Expressions;

namespace CipherApp.Test.Services
{
    public class MessageServiceTests
    {
        private readonly IMessageRepository _msgRepository;
        private readonly IChatRepository _chatRepository;
        private readonly ILogger<MessageService> _logger;
        private readonly IMapper _mapper;

        private readonly IMessageService _service;

        public MessageServiceTests()
        {
            _msgRepository = Substitute.For<IMessageRepository>();
            _chatRepository = Substitute.For<IChatRepository>();
            _logger = Substitute.For<ILogger<MessageService>>();
            _mapper = Substitute.For<IMapper>();

            _service = new MessageService(_mapper, _logger, _msgRepository, _chatRepository);
        }

        [Fact]
        public async Task AddMessageAsync_WhenSuccess_ReturnsDto()
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
                        Messages = new List<Message>(),
                        Chats = new List<Chat>()
                    }
                },
            };

            _chatRepository
                .GetByQueryAsync(
                    Arg.Any<Expression<Func<Chat, bool>>>(), 
                    Arg.Any<Expression<Func<Chat, object>>[]>())
                .Returns(mockChat);

            _msgRepository
                .AddEntityAsync(Arg.Any<Message>())
                .Returns(TestEntities._mockMessage);

            _mapper
                .Map<MessageDto>(Arg.Any<Message>())
                .Returns(TestEntities._mockMessageDto);

            var result = await _service.AddMessageAsync("ABC1234", "Hello World!", 1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddMessageAsync_WhenChatDoesNotExist_Throws_NotFoundException()
        {
            await Assert.ThrowsAsync<NotFoundException>(
                () => _service.AddMessageAsync("ABC1234", "Hello World!", 1));
        }

        [Fact]
        public async Task AddMessageAsync_WhenUserNotInChat_Throws_NotFoundException()
        {
            Chat mockChat = new()
            {
                Id = 1,
                UID = "1234ABC",
                LastMessage = new DateTime(),
                CreatedAt = new DateTime(),
                Messages = new List<Message>(),
                Users = new List<User>()
            };

            _chatRepository
                .GetByQueryAsync(
                    Arg.Any<Expression<Func<Chat, bool>>>(),
                    Arg.Any<Expression<Func<Chat, object>>[]>())
                .Returns(mockChat);

            await Assert.ThrowsAsync<NotFoundException>(
                () => _service.AddMessageAsync("ABC1234", "Hello World!", 1));
        }
    }
}

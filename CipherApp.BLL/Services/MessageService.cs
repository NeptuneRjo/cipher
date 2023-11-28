using AutoMapper;
using CipherApp.BLL.Services.IServices;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Entities;
using CipherApp.DAL.Models;
using CipherApp.DAL.Repositories.IRepositories;
using CipherApp.DTO.Response;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace CipherApp.BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MessageService> _logger;

        private readonly IMessageRepository _msgRepository;
        private readonly IChatRepository _chatRepository;

        public MessageService(
            IMapper mapper,
            ILogger<MessageService> logger,
            IMessageRepository msgRepository,
            IChatRepository chatRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _msgRepository = msgRepository;
            _chatRepository = chatRepository;
        }

        private readonly Expression<Func<Message, object>>[] includes = 
        { 
            e => e.User, 
            e => e.Chat 
        };

        private async Task<Chat> GetChatAsync(string chatUID)
        {
            Expression<Func<Chat, object>>[] chatQueryIncludes = 
            { 
                chat => chat.Users, 
                chat => chat.Messages 
            };
            Chat chat = await _chatRepository
                .GetByQueryAsync(chat => chat.UID == chatUID, chatQueryIncludes);

            if (chat == null)
            {
                throw new NotFoundException($"Chat \"{chatUID}\" was not found");
            }

            return chat;
        }

        private async Task<Message> CreateMessageAsync(string content, Chat chat, User user)
        {
            Message message = new()
            {
                Content = content,
                UserId = user.Id,
                ChatId = chat.Id,
                CreatedAt = DateTime.Now,
                User = user,
                Chat = chat
            };

            Message addedMessage = await _msgRepository.AddEntityAsync(message);

            return addedMessage;
        }

        private User GetUserFromChat(Chat chat, int userId)
        {
            try
            {
                return chat.Users.First(user => user.Id == userId);
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException(
                    $"No user with the id \"{userId}\" was found in chat \"{chat.UID}\"");
            }
        }

        public async Task<MessageDto> AddMessageAsync(string chatUID, string content, int userId)
        {
            Chat chat = await GetChatAsync(chatUID);

            User user = GetUserFromChat(chat, userId);

            Message message = await CreateMessageAsync(content, chat, user);

            return _mapper.Map<MessageDto>(message);
        }
    }
}

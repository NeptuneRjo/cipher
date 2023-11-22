using AutoMapper;
using CipherApp.BLL.Services.IServices;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Entities;
using CipherApp.DAL.Repositories.IRepositories;
using CipherApp.DTO.Response;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace CipherApp.BLL.Services
{
    public class ChatService : IChatService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ChatService> _logger;
        private readonly IChatRepository _repository;

        public ChatService(
            IMapper mapper, 
            ILogger<ChatService> logger, 
            IChatRepository repository
            )
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }

        private readonly Expression<Func<Chat, object>>[] includes =
        {
            e => e.Messages, e => e.Users
        };

        private async Task<Chat> GetChatByUidAsync(string UID)
        {
            Chat chat = await _repository.GetByQueryAsync(e => e.UID == UID, includes);

            if (chat == null)
            {
                _logger.LogError($"Chat with the UID = {UID} was not found");
                throw new NotFoundException();
            }

            return chat;
        }

        public async Task<ChatDto> GetChatAsync(string UID)
        {
            Chat chat = await GetChatByUidAsync(UID);

            return _mapper.Map<ChatDto>(chat);
        }

        public async Task<ICollection<ChatDto>> GetChatsByUserAsync(string email)
        {
            ICollection<Chat> chats = await _repository.GetAllByQueryAsync(e => e.Users.Any(u => u.Email == email), includes);

            return _mapper.Map<ICollection<ChatDto>>(chats);
        }

        public async Task<ChatDto> CreateChatAsync(string email)
        {

            Chat chat = await _repository.CreateChatByEmail(email);

            return _mapper.Map<ChatDto>(chat);
        }

        public async Task<ChatDto> AddUserAsync(string email, string chatUID)
        {
            Chat chat = await _repository.AddUserToChat(email, chatUID);

            return _mapper.Map<ChatDto>(chat);
        }

        public async Task<bool> ChatExistsAsync(string chatUID) =>
            await _repository.ExistsAsync(chat => chat.UID == chatUID);

        public async Task RemoveChatByUserAsync(string email, string UID) => 
            await _repository.RemoveUserFromChat(email, UID); 
    }
}

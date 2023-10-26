using AutoMapper;
using CipherApp.BLL.Services.IServices;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Entities;
using CipherApp.DAL.Repositories.IRepositories;
using CipherApp.DTO.Request;
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
            e => e.Messages
        };

        public async Task<ChatDto> GetChatAsync(int id, string username)
        {
            var chat = await _repository.GetByQueryAsync(e => e.Id == id, includes);

            if (chat == null)
            {
                _logger.LogError($"Chat with the id = {id} was not found");
                throw new NotFoundException();
            }

            var chatDto = _mapper.Map<ChatDto>(chat);

            return chatDto;
        }

        public async Task<ChatDto> CreateChatAsync
            (ChatToCreateDto chatToCreate, int userId)
        {
            Chat chat = new()
            {
                //OwnerId = userId,
                //Name = chatToCreate?.Name,
            };

            Chat addedChat = await _repository.AddEntityAsync(chat);

            ChatDto chatDto = _mapper.Map<ChatDto>(addedChat);

            return chatDto;
        }

        public async Task<ICollection<ChatDto>> GetChatsAsync(int userUID)
        {
            var chats = await _repository.GetAllByQueryAsync(
                e => e.ParticipantOneId == userUID || 
                e.ParticipantTwoId == userUID,
                includes);

            ICollection<ChatDto> chatDtoList = _mapper.Map<ICollection<ChatDto>>(chats);

            return chatDtoList;
        }

    }
}

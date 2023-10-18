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
    public class ChatUserService : IChatUserService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IChatUserRepository _cuRepository;

        private readonly IMapper _mapper;
        private readonly ILogger<ChatUserService> _logger;

        public ChatUserService(
            IChatRepository chatRepository, 
            IUserRepository userRepository, 
            IMapper mapper, 
            ILogger<ChatUserService> logger,
            IChatUserRepository cuRepository
            )
        {
            _chatRepository = chatRepository;
            _userRepository = userRepository;
            _cuRepository = cuRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateChatUserAsync(int chatId, string username)
        {
            User user = await _userRepository
                .GetByQueryAsync(e => e.Username.ToLower() == username.ToLower());

            Chat chat = await _chatRepository.GetByQueryAsync(e => e.Id == chatId);

            if (user == null || chat == null)
                throw new NotFoundException();

            ChatUser chatUser = new()
            {
                UserId = user.Id,
                ChatId = chat.Id,
                User = user,
                Chat = chat
            };

            await _cuRepository.AddEntityAsync(chatUser);
        }
    }
}

using AutoMapper;
using CipherApp.BLL.Services.IServices;
using CipherApp.DAL.Repositories.IRepositories;
using Microsoft.Extensions.Logging;

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
    }
}

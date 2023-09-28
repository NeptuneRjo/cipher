using AutoMapper;
using CipherApp.BLL.Services.IServices;
using CipherApp.DAL.Repositories.IRepositories;
using Microsoft.Extensions.Logging;

namespace CipherApp.BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MessageService> _logger;
        private readonly IMessageRepository _repository;

        public MessageService(
            IMapper mapper,
            ILogger<MessageService> logger,
            IMessageRepository repository
            )
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }

    }
}

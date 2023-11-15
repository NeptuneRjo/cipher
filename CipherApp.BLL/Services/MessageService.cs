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

        private readonly Expression<Func<Message, object>>[] includes = 
        { 
            e => e.User, 
            e => e.Chat 
        };

        public async Task<MessageDto> AddMessageAsync(string chatUID, string content, int userId)
        {
            Message msg = await _repository.CreateAndAddToChatAsync(chatUID, content, userId);
            
            return _mapper.Map<MessageDto>(msg);
        }
    }
}

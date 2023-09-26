using CipherApp.BLL.Operations.IOperations;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Entities;
using CipherApp.DAL.Repositories.IRepositories;
using Microsoft.Extensions.Logging;

namespace CipherApp.BLL.Operations
{
    public class ChatOperations : IChatOperations
    {
        private readonly ILogger<ChatOperations> _logger;
        private readonly IChatRepository _repository;

        public ChatOperations(ILogger<ChatOperations> logger, IChatRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
    }
}

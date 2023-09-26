using CipherApp.BLL.Operations.IOperations;
using CipherApp.DAL.Repositories.IRepositories;
using Microsoft.Extensions.Logging;

namespace CipherApp.BLL.Operations
{
    public class MessageOperations : IMessageOperations
    {
        private readonly IMessageRepository _repository;
        private readonly ILogger<MessageOperations> _logger;

        public MessageOperations(IMessageRepository repository, ILogger<MessageOperations> logger)
        {
            _logger = logger;
            _repository = repository;
        }

    }
}

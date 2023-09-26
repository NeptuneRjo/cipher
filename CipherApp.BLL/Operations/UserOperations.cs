using CipherApp.BLL.Operations.IOperations;
using CipherApp.DAL.Repositories.IRepositories;
using Microsoft.Extensions.Logging;

namespace CipherApp.BLL.Operations
{
    public class UserOperations : IUserOperations
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<UserOperations> _logger;

        public UserOperations(IUserRepository repository, ILogger<UserOperations> logger)
        {
            _repository = repository;
            _logger = logger;
        }

    }
}

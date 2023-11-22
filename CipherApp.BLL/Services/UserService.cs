using AutoMapper;
using CipherApp.BLL.Services.IServices;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Entities;
using CipherApp.DAL.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace CipherApp.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _repository;

        private readonly Expression<Func<User, object>>[] includes = { 
            e => e.Chats, e => e.Messages
        };

        public UserService(
            IMapper mapper,
            ILogger<UserService> logger,
            IUserRepository repository
            )
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<User> GetUserAsync(string email)
        {
            _logger.LogInformation($"User requested with the email = {email}");
            var user = await _repository.GetByQueryAsync(e => e.Email == email, includes);

            if (user == null)
            {
                _logger.LogError($"User with the email = {email} was not found");
                throw new NotFoundException();
            }

            return user;
        }

        public async Task<User> GetUserAsync(int id)
        {
            _logger.LogInformation($"User requested with id = {id}");
            var user = await _repository.GetByQueryAsync(e => e.Id == id, includes);

            if (user == null)
            {
                _logger.LogError($"User with the id = {id} was not found");
                throw new NotFoundException();
            }

            return user;
        }
    }
}

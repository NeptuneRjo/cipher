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

        public async Task<UserDto> AuthUserAsync(string username, string password) =>
            AuthenticateAndMapUser(await GetUserByUsername(username), password);

        public async Task<UserDto> AuthUserAsync(int id, string password) =>
            AuthenticateAndMapUser(await GetUserById(id), password);

        public async Task<User> GetUserAsync(string email) =>
            await GetUserByEmail(email);

        public async Task<User> GetUserAsync(int id) =>
            await GetUserById(id);

        private UserDto AuthenticateAndMapUser(User user, string password)
        {
            if (user.ValidatePassword(password))
                return _mapper.Map<UserDto>(user);

            throw new UnauthorizedAccessException();
        }

        private async Task<User> GetUserByUsername(string username)
        {
            _logger.LogInformation($"User requested with username = {username}");
            var user = await _repository.GetByQueryAsync(e => e.Username == username, includes);

            if (user == null)
            {
                _logger.LogError($"User with the username = {username} was not found");
                throw new NotFoundException();
            }

            return user;
        }

        private async Task<User> GetUserByEmail(string email)
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

        private async Task<User> GetUserById(int id)
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

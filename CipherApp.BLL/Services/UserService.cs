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
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _repository;

        private readonly Expression<Func<User, object>>[] includes = { 

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

        public async Task<UserDto> CreateUserAsync(UserToRegisterDto newUser)
        {
            var user = _mapper.Map<User>(newUser);

            //user.EncryptPassword();

            var added = await _repository.AddEntityAsync(user);

            return _mapper.Map<UserDto>(added);
        }

        public async Task<UserDto> AuthUserAsync(string username, string password) =>
            AuthenticateAndMapUser(await GetUserByUsername(username), password);

        public async Task<UserDto> AuthUserAsync(int id, string password) =>
            AuthenticateAndMapUser(await GetUserById(id), password);

        public async Task<UserDto> GetUserAsync(string username) =>
            _mapper.Map<UserDto>(await GetUserByUsername(username));

        public async Task<UserDto> GetUserAsync(int id) =>
            _mapper.Map<UserDto>(await GetUserById(id));

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

using AutoMapper;
using CipherApp.BLL.Services.IServices;
using CipherApp.DAL.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Entities;
using CipherApp.DTO.Response;
using CipherApp.DAL.Models;

namespace CipherApp.BLL.Services
{

    using BCrypt.Net;

    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            IMapper mapper, 
            ILogger<AuthService> logger, 
            IUserRepository repository)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }

        private async Task CheckIfUserExists(string email)
        {
            bool exists = await _repository.ExistsAsync(e => e.Email == email);

            if (exists)
            {
                throw new UserExistsException();
            }
        }

        private string EncryptPassword(string password) =>
            BCrypt.HashPassword(password, BCrypt.GenerateSalt());

        private async Task<User> GetUserByEmailAsync(string email)
        {
            User user = await _repository
                .GetByQueryAsync(e => e.Email == email);

            if (user == null)
            {
                _logger.LogError($"User with the email = {email} was not found");
                throw new NotFoundException();
            }

            return user;
        }

        private void ValidateUser(User user, string password)
        {
            if(!BCrypt.Verify(password, user.Password))
            {
                throw new LoginFailedException();
            }
        }

        public async Task<UserDto> LoginAsync(LoginInputModel input)
        {
            User user = await GetUserByEmailAsync(input.Email);

            ValidateUser(user, input.Password);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> RegisterAsync(RegisterInputModel input)
        {
            await CheckIfUserExists(input.Email);

            User user = _mapper.Map<User>(input);
            user.Password = EncryptPassword(user.Password);

            await _repository.AddEntityAsync(user);

            return _mapper.Map<UserDto>(user);
        }
    }
}

using AutoMapper;
using CipherApp.BLL.Services.IServices;
using CipherApp.DAL.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Entities;
using CipherApp.DTO.Response;
using Microsoft.Extensions.Configuration;
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

        public async Task<UserDto> LoginAsync(LoginInputModel input)
        {
            User user = await _repository
                .GetByQueryAsync(e => e.Email == input.Email);

            if (user == null)
            {
                _logger.LogError($"User with the email = {input.Email} was not found");
                throw new NotFoundException();
            }

            bool validated = user.ValidatePassword(input.Password);

            if (!validated)
                throw new LoginFailedException();

            UserDto userToReturn = _mapper.Map<UserDto>(user);

            return userToReturn;
        }

        public async Task<UserDto> RegisterAsync(RegisterInputModel input)
        {

            bool alreadyInUse = await _repository
                .ExistsAsync(e => e.Email == input.Email);

            if (alreadyInUse)
                throw new UserExistsException();

            User user = _mapper.Map<User>(input);
            user.Password = EncryptPassword(user.Password);

            await _repository.AddEntityAsync(user);

            UserDto userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        private string EncryptPassword(string password) =>
            BCrypt.HashPassword(password, BCrypt.GenerateSalt());
    }
}

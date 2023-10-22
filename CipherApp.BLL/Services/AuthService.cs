using AutoMapper;
using CipherApp.BLL.Services.IServices;
using CipherApp.DAL.Repositories.IRepositories;
using CipherApp.DTO.Request;
using Microsoft.Extensions.Logging;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Entities;
using CipherApp.DTO.Response;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace CipherApp.BLL.Services
{

    using BCrypt.Net;
    using CipherApp.DAL.Models;

    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;

        private readonly IConfiguration _config;

        public AuthService(
            IMapper mapper, 
            ILogger<AuthService> logger, 
            IUserRepository repository,
            IConfiguration config
            )
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;

            _config = config;
        }

        public async Task<UserDto> LoginAsync(LoginInputModel input)
        {
            var user = await _repository
                .GetByQueryAsync(e => e.Username == input.Username);

            if (user == null)
            {
                _logger.LogError($"User with the username = {input.Username} was not found");
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
                .ExistsAsync(e => e.Username.ToLower() == input.Username.ToLower());

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

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

        public async Task<UserDto> LoginAsync(UserToLoginDto userToLoginDto)
        {
            var user = await _repository
                .GetByQueryAsync(e => e.Username == userToLoginDto.username);

            if (user == null)
            {
                _logger.LogError($"User with the username = {userToLoginDto.username} was not found");
                throw new NotFoundException();
            }

            bool validated = user.ValidatePassword(userToLoginDto.password);

            if (!validated)
                throw new LoginFailedException();

            var userToReturn = _mapper.Map<UserDto>(user);
            userToReturn.Token = GenerateJWT(user.Id, user.Username);

            return userToReturn;
        }

        public async Task<User> RegisterAsync(UserToRegisterDto userToRegisterDto)
        {
            User user = _mapper.Map<User>(userToRegisterDto);

            bool alreadyInUse = await _repository.ExistsAsync(e => e.Username.ToLower() == userToRegisterDto.Username.ToLower());

            if (alreadyInUse)
                throw new UserExistsException();

            user.Password = EncryptPassword(user.Password);

            await _repository.AddEntityAsync(user);

            return user;
        }

        private string GenerateJWT(int userId, string username)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, username)
            };
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        private string EncryptPassword(string password) =>
            BCrypt.HashPassword(password, BCrypt.GenerateSalt());
    }
}

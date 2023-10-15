using AutoMapper;
using CipherApp.BLL.Services;
using CipherApp.BLL.Services.IServices;
using CipherApp.DAL.Entities;
using CipherApp.DAL.Repositories.IRepositories;
using CipherApp.DTO.Request;
using CipherApp.DTO.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Linq.Expressions;
using Xunit.Abstractions;


namespace CipherApp.Test.Services
{
    using BCrypt.Net;
    using CipherApp.BLL.Utilities.CustomExceptions;

    public class AuthServiceTests
    {
        private readonly IAuthService _service;

        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;
        private readonly IConfiguration _config;

        private readonly ITestOutputHelper _output;

        private readonly User _mockUser = new()
        {
            Id = 1,
            Password = BCrypt.HashPassword("password", BCrypt.GenerateSalt()),
            Username = "username",
            CreatedAt = new DateTime(),
            ChatUsers = new List<ChatUser>(),
            Messages = new List<Message>(),
        };

        private readonly UserDto _mockUserDto = new()
        {
            Id = 1,
            Username = "username",
            CreatedAt = new DateTime(),
            Chats = new List<ChatDto>(),
            Messages = new List<MessageDto>(),
        };

        private readonly UserToLoginDto _mockUserToLogin = new UserToLoginDto()
        {
            username = "username",
            password = "password"
        };

        private readonly UserToRegisterDto _mockUserToRegister = new UserToRegisterDto()
        {
            Username = "username",
            Password = "password"
        };

        public AuthServiceTests(ITestOutputHelper output)
        {
            _output = output;

            _logger = Substitute.For<ILogger<AuthService>>();
            _repository = Substitute.For<IUserRepository>();
            _mapper = Substitute.For<IMapper>();
            _config = Substitute.For<IConfiguration>();

            _service = new AuthService(_mapper, _logger, _repository, _config);

            _config["Jwt:Key"].Returns("dyJhbGciOiJIUzI1Ni99.eyJSb3dlIjoiQWRtaW4iLCJJc3N1ZXIiOiJJc3N1ZXIiLCJVc2VybmFtZSI6IkphdmFJblVzZSIsImV4cCD5TY5MzE3OTg4MiwiaWF0IjoxNjkzMTc5ODgyfQ.lww5Z0eGLvW2h_GXjnDZXnF_3ld2WADb3cye-fTaIlU");
        }

        [Fact]
        public async Task LoginAsync_WhenSuccess_ReturnsDto_WithToken()
        {
            _repository
                .GetByQueryAsync(
                    Arg.Any<Expression<Func<User, bool>>>(),
                    Arg.Any<Expression<Func<User, object>>[]>()
                    )
                .Returns(Task.FromResult(_mockUser));
            _mapper.Map<UserDto>(_mockUser).Returns(_mockUserDto);

            var result = await _service.LoginAsync(_mockUserToLogin);

            Assert.NotNull(result);
            Assert.NotNull(result.Token);
        }

        [Fact]
        public async Task LoginAsync_WhenInvalidPassword_ThrowsBcryptAuthenticationException()
        {
            _repository
                .GetByQueryAsync(
                    Arg.Any<Expression<Func<User, bool>>>(),
                    Arg.Any<Expression<Func<User, object>>[]>()
                    )
                .Returns(Task.FromResult(_mockUser));
            _mapper.Map<UserDto>(_mockUser).Returns(_mockUserDto);

            await Assert.ThrowsAsync<BcryptAuthenticationException>(
                () => _service.LoginAsync(new UserToLoginDto()
                {
                    username = "username",
                    password = "test"
                }));
        }

        [Fact]
        public async Task LoginAsync_WhenUserNotFound_ThrowsNotFoundException()
        {
            await Assert.ThrowsAsync<NotFoundException>(
                () => _service.LoginAsync(_mockUserToLogin));
        }

        [Fact]
        public async Task RegisterAsync_WhenSuccessful_ReturnsUser()
        {
            var userToAdd = new User()
            {
                Id = 1,
                Password = "password",
                Username = "username",
                CreatedAt = new DateTime(),
                ChatUsers = new List<ChatUser>(),
                Messages = new List<Message>(),
            };

            _mapper.Map<User>(_mockUserToRegister).Returns(_mockUser);
            _repository
                .AddEntityAsync(Arg.Any<User>())
                .Returns(userToAdd);

            var result = await _service.RegisterAsync(_mockUserToRegister);

            Assert.NotNull(result);
            Assert.Equivalent(result, _mockUser, strict: true);
        }
    }
}

using AutoMapper;
using CipherApp.BLL.Services;
using CipherApp.BLL.Services.IServices;
using CipherApp.DAL.Entities;
using CipherApp.DAL.Repositories.IRepositories;
using CipherApp.DTO.Response;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Linq.Expressions;
using Xunit.Abstractions;


namespace CipherApp.Test.Services
{
    using BCrypt.Net;
    using CipherApp.BLL.Utilities.CustomExceptions;
    using CipherApp.DAL.Models;

    public class AuthServiceTests
    {
        private readonly IAuthService _service;

        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;

        private readonly ITestOutputHelper _output;

        public AuthServiceTests(ITestOutputHelper output)
        {
            _output = output;

            _logger = Substitute.For<ILogger<AuthService>>();
            _repository = Substitute.For<IUserRepository>();
            _mapper = Substitute.For<IMapper>();

            _service = new AuthService(_mapper, _logger, _repository);
        }

        private readonly User _mockUser = new()
        {
            Id = 1,
            Username = "testUsername",
            Email = "test@email.com",
            Password = BCrypt.HashPassword("password"),
            Messages = new List<Message>(),
            Chats = new List<Chat>(),
        };

        private readonly UserDto _mockUserDto = new()
        {
            Id = 1,
            Username = "testUsername",
            Email = "test@email.com",
        };

        private readonly LoginInputModel _loginModel = new()
        {
            Email = "test@email.com",
            Password = "password",
        };

        private readonly RegisterInputModel _registerModel = new()
        {
            Username = "testUsername",
            Email = "test@email.com",
            Password = "password",
            ConfirmPassword = "password"
        };

        [Fact]
        public async Task LoginAsync_WhenSuccess_ReturnsDto()
        {
            _repository
                .GetByQueryAsync(
                    Arg.Any<Expression<Func<User, bool>>>(),
                    Arg.Any<Expression<Func<User, object>>[]>())
                .Returns(_mockUser);

            _mapper.Map<UserDto>(_mockUser).Returns(_mockUserDto);

            var result = await _service.LoginAsync(_loginModel);

            Assert.NotNull(result);
            Assert.Equivalent(result, _mockUserDto, strict: true);
        }

        [Fact]
        public async Task LoginAsync_WhenInvalidPassword_ThrowsLoginFailedException()
        {
            _repository
                .GetByQueryAsync(
                    Arg.Any<Expression<Func<User, bool>>>(),
                    Arg.Any<Expression<Func<User, object>>[]>())
                .Returns(_mockUser);

            LoginInputModel failingInputModel = new()
            {
                Email = "test@email.com",
                Password = "incorrectPassword"
            };

            await Assert.ThrowsAsync<LoginFailedException>(
                () => _service.LoginAsync(failingInputModel));
        }

        [Fact]
        public async Task LoginAsync_WhenUserNotFound_ThrowsNotFoundException()
        {
            await Assert.ThrowsAsync<NotFoundException>(
                () => _service.LoginAsync(_loginModel));
        }

        [Fact]
        public async Task RegisterAsync_WhenSuccessful_ReturnsUser()
        {
            _repository
                .ExistsAsync(Arg.Any<Expression<Func<User, bool>>>())
                .Returns(false);

            _repository
                .AddEntityAsync(Arg.Any<User>())
                .Returns(_mockUser);

            _mapper.Map<User>(Arg.Any<RegisterInputModel>()).Returns(_mockUser);
            _mapper.Map<UserDto>(Arg.Any<User>()).Returns(_mockUserDto);

            var result = await _service.RegisterAsync(_registerModel);

            Assert.NotNull(result);
            Assert.Equivalent(result, _mockUserDto);
        }

        [Fact]
        public async Task RegisterAsync_WhenUsernameInUser_ThrowsUserExistsException()
        {
            _repository
                .ExistsAsync(Arg.Any<Expression<Func<User, bool>>>())
                .Returns(true);

            await Assert.ThrowsAsync<UserExistsException>(
                () => _service.RegisterAsync(_registerModel));
        }
    }
}

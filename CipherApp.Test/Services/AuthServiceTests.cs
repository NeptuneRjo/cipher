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
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Models;

namespace CipherApp.Test.Services
{
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

        [Fact]
        public async Task LoginAsync_WhenSuccess_ReturnsDto()
        {
            _repository
                .GetByQueryAsync(
                    Arg.Any<Expression<Func<User, bool>>>(),
                    Arg.Any<Expression<Func<User, object>>[]>())
                .Returns(TestEntities._mockUser);

            _mapper
                .Map<UserDto>(TestEntities._mockUser)
                .Returns(TestEntities._mockUserDto);

            var result = await _service.LoginAsync(TestEntities._mockLoginModel);

            Assert.NotNull(result);
            Assert.Equivalent(result, TestEntities._mockUserDto, strict: true);
        }

        [Fact]
        public async Task LoginAsync_WhenInvalidPassword_ThrowsLoginFailedException()
        {
            _repository
                .GetByQueryAsync(
                    Arg.Any<Expression<Func<User, bool>>>(),
                    Arg.Any<Expression<Func<User, object>>[]>())
                .Returns(TestEntities._mockUser);

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
                () => _service.LoginAsync(TestEntities._mockLoginModel));
        }

        [Fact]
        public async Task RegisterAsync_WhenSuccessful_ReturnsUser()
        {
            _repository
                .ExistsAsync(Arg.Any<Expression<Func<User, bool>>>())
                .Returns(false);

            _repository
                .AddEntityAsync(Arg.Any<User>())
                .Returns(TestEntities._mockUser);

            _mapper
                .Map<User>(Arg.Any<RegisterInputModel>())
                .Returns(TestEntities._mockUser);
            
            _mapper
                .Map<UserDto>(Arg.Any<User>())
                .Returns(TestEntities._mockUserDto);

            var result = await _service.RegisterAsync(TestEntities._mockRegisterModel);

            Assert.NotNull(result);
            Assert.Equivalent(result, TestEntities._mockUserDto);
        }

        [Fact]
        public async Task RegisterAsync_WhenUsernameInUser_ThrowsUserExistsException()
        {
            _repository
                .ExistsAsync(Arg.Any<Expression<Func<User, bool>>>())
                .Returns(true);

            await Assert.ThrowsAsync<UserExistsException>(
                () => _service.RegisterAsync(TestEntities._mockRegisterModel));
        }
    }
}

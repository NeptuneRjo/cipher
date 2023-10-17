using AutoMapper;
using CipherApp.BLL.Services;
using CipherApp.BLL.Services.IServices;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Entities;
using CipherApp.DAL.Repositories.IRepositories;
using CipherApp.DTO.Response;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Linq.Expressions;
using Xunit.Abstractions;

namespace CipherApp.Test.Services
{
    public class UserServiceTests
    {
        private readonly IUserService _service;
        private readonly IUserRepository _repository;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;

        private readonly ITestOutputHelper _output;

        private readonly User _mockUser = new()
        {
            Id = 1,
            Password = "password",
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
            Chats = new List<ChatListDto>(),
            Messages = new List<MessageDto>(),
        };

        public UserServiceTests(ITestOutputHelper output)
        {
            _mapper = Substitute.For<IMapper>();
            _logger = Substitute.For<ILogger<UserService>>();
            _repository = Substitute.For<IUserRepository>();

            _service = new UserService(_mapper, _logger, _repository);
        
            _output = output;
        }

        [Fact]
        public async Task GetUserAsync_ByUsername_WhenSuccess_ReturnsDto()
        {
            _repository
                .GetByQueryAsync(
                    Arg.Any<Expression<Func<User, bool>>>(), 
                    Arg.Any<Expression<Func<User, object>>[]>()
                    )
                .Returns(Task.FromResult(_mockUser));

            _mapper
                .Map<UserDto>(_mockUser)
                .Returns(_mockUserDto);

            var result = await _service.GetUserAsync("test");

            Assert.NotNull(result);
            Assert.Equivalent(result, _mockUserDto, strict: true);
        }

        [Fact]
        public async Task GetUserAsync_ById_WhenSuccess_ReturnsDto()
        {
            _repository
                .GetByQueryAsync(
                    Arg.Any<Expression<Func<User, bool>>>(),
                    Arg.Any<Expression<Func<User, object>>[]>()
                    )
                .Returns(Task.FromResult(_mockUser));

            _mapper
                .Map<UserDto>(_mockUser)
                .Returns(_mockUserDto);

            var result = await _service.GetUserAsync(1);

            Assert.NotNull(result);
            Assert.Equivalent(result, _mockUserDto, strict: true);
        }

        [Fact]
        public async Task GetUserAsync_ByUsername_WhenNotFound_ThrowsNotFoundException()
        {
            Assert.ThrowsAsync<NotFoundException>(() => _service.GetUserAsync("test"));
        }

        [Fact]
        public async Task GetUserAsync_ById_WhenNotFound_ThrowsNotFoundException()
        {
            Assert.ThrowsAsync<NotFoundException>(() => _service.GetUserAsync(1));
        }

        [Fact]
        public async Task AuthUserAsync_ByUsername_WhenMatch_ReturnsDto()
        {
            _repository
                .GetByQueryAsync(
                    Arg.Any<Expression<Func<User, bool>>>(),
                    Arg.Any<Expression<Func<User, object>>[]>()
                    )
                .Returns(Task.FromResult(_mockUser));

            _mapper
                .Map<UserDto>(_mockUser)
                .Returns(_mockUserDto);

            var result = await _service
                .AuthUserAsync(_mockUser.Username, _mockUser.Password);

            Assert.NotNull(result);
        }
    }
}

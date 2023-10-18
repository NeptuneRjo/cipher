using AutoMapper;
using CipherApp.BLL.Services;
using CipherApp.BLL.Services.IServices;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Entities;
using CipherApp.DAL.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Linq.Expressions;

namespace CipherApp.Test.Services
{
    public class ChatUserServiceTests
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IChatUserRepository _cuRepository;

        private readonly IMapper _mapper;
        private readonly ILogger<ChatUserService> _logger;

        private readonly IChatUserService _cuService;

        public ChatUserServiceTests()
        {
            _chatRepository = Substitute.For<IChatRepository>();
            _userRepository = Substitute.For<IUserRepository>();
            _cuRepository = Substitute.For<IChatUserRepository>();

            _mapper = Substitute.For<IMapper>();
            _logger = Substitute.For<ILogger<ChatUserService>>();

            _cuService = new ChatUserService
                (
                    _chatRepository, 
                    _userRepository, 
                    _mapper, 
                    _logger, 
                    _cuRepository
                );            
        }

        private readonly User _mockUser = new User()
        {
            Id = 1,
        };

        private readonly Chat _mockChat = new Chat()
        {
            Id = 1,
        };

        [Fact]
        public async Task CreateChatUserAsync_WhenSuccess_CreatsTheChatUser()
        {
            _userRepository
                .GetByQueryAsync(Arg.Any<Expression<Func<User, bool>>>())
                .Returns(Task.FromResult(_mockUser));

            _chatRepository
                .GetByQueryAsync(Arg.Any<Expression<Func<Chat, bool>>>())
                .Returns(Task.FromResult(_mockChat));

            await _cuService.CreateChatUserAsync(1, "test");

            await _cuRepository
                .Received()
                .AddEntityAsync(Arg.Is<ChatUser>(cu => cu.UserId == _mockUser.Id));
        }

        [Fact]
        public async Task CreateChatAsync_WhenChatIsNull_ThrowsNotFoundException()
        {
            _userRepository
                .GetByQueryAsync(Arg.Any<Expression<Func<User, bool>>>())
                .Returns(Task.FromResult(_mockUser));

            _chatRepository
                .GetByQueryAsync(Arg.Any<Expression<Func<Chat, bool>>>())
                .ReturnsNull();

            await Assert.ThrowsAsync<NotFoundException>(
                () => _cuService.CreateChatUserAsync(1, "test")
                );
        }

        [Fact]
        public async Task CreateChatAsync_WhenUserIsNull_ThrowsNotFoundException()
        {
            _userRepository
                .GetByQueryAsync(Arg.Any<Expression<Func<User, bool>>>())
                .ReturnsNull();

            _chatRepository
                .GetByQueryAsync(Arg.Any<Expression<Func<Chat, bool>>>())
                .ReturnsNull();

            await Assert.ThrowsAsync<NotFoundException>(
                () => _cuService.CreateChatUserAsync(1, "test")
                );
        }
    }
}

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

        public UserServiceTests(ITestOutputHelper output)
        {
            _mapper = Substitute.For<IMapper>();
            _logger = Substitute.For<ILogger<UserService>>();
            _repository = Substitute.For<IUserRepository>();

            _service = new UserService(_mapper, _logger, _repository);
        
            _output = output;
        }

        [Fact]
        public async Task GetUserAsync_ByEmail_WhenSuccess_ReturnsDto()
        {
            _repository
                .GetByQueryAsync(
                    Arg.Any<Expression<Func<User, bool>>>(), 
                    Arg.Any<Expression<Func<User, object>>[]>())
                .Returns(TestEntities._mockUser);

            var result = await _service.GetUserAsync("test@email.com");

            Assert.NotNull(result);
            Assert.Equivalent(result, TestEntities._mockUser, strict: true);
        }

        [Fact]
        public async Task GetUserAsync_ById_WhenSuccess_ReturnsDto()
        {
            _repository
                .GetByQueryAsync(
                    Arg.Any<Expression<Func<User, bool>>>(),
                    Arg.Any<Expression<Func<User, object>>[]>())
                .Returns(TestEntities._mockUser);

            var result = await _service.GetUserAsync(1);

            Assert.NotNull(result);
            Assert.Equivalent(result, TestEntities._mockUser, strict: true);
        }

        [Fact]
        public async Task GetUserAsync_ByEmail_WhenNotFound_ThrowsNotFoundException()
        {
            await Assert.ThrowsAsync<NotFoundException>(
                () => _service.GetUserAsync(""));
        }

        [Fact]
        public async Task GetUserAsync_ById_WhenNotFound_ThrowsNotFoundException()
        {
            await Assert.ThrowsAsync<NotFoundException>(
                () => _service.GetUserAsync(1));
        }
    }
}

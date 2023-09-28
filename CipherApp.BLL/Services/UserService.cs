using AutoMapper;
using CipherApp.BLL.Services.IServices;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Entities;
using CipherApp.DAL.Repositories.IRepositories;
using CipherApp.DTO.Response;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace CipherApp.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _repository;

        private readonly Expression<Func<User, object>>[] includes = { 
            e => e.ChatUsers, e=> e.Messages 
        };

        public UserService(
            IMapper mapper,
            ILogger<UserService> logger,
            IUserRepository repository
            )
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUserAsync(string uid)
        {
            _logger.LogInformation($"User requested with uid = {uid}");
            var user = await _repository.GetByQueryAsync(e => e.UID == uid, includes);

            if (user == null)
            {
                _logger.LogError($"User with the uid = {uid} was not found");
                throw new NotFoundException();
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserAsync(int id)
        {
            _logger.LogInformation($"User requested with id = {id}");
            var user = await _repository.GetByQueryAsync(e => e.Id == id, includes);

            if (user == null)
            {
                _logger.LogError($"User with the id = {id} was not found");
                throw new NotFoundException();
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}

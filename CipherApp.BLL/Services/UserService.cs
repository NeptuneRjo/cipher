using AutoMapper;
using CipherApp.BLL.Operations.IOperations;
using CipherApp.BLL.Services.IServices;

namespace CipherApp.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserOperations _ops;
        private readonly IMapper _mapper;

        public UserService(IUserOperations ops, IMapper mapper)
        {
            _ops = ops;
            _mapper = mapper;
        }

    }
}

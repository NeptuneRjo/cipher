using AutoMapper;
using CipherApp.BLL.Operations.IOperations;
using CipherApp.BLL.Services.IServices;

namespace CipherApp.BLL.Services
{
    public class ChatService : IChatService
    {
        private readonly IMapper _mapper;
        private readonly IChatOperations _ops;

        public ChatService(IMapper mapper, IChatOperations ops)
        {
            _mapper = mapper;
            _ops = ops;
        }

    }
}

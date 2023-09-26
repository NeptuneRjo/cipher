using AutoMapper;
using CipherApp.BLL.Operations.IOperations;
using CipherApp.BLL.Services.IServices;

namespace CipherApp.BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageOperations _ops;
        private readonly IMapper _mapper;

        public MessageService(IMessageOperations ops, IMapper mapper)
        {
            _mapper = mapper;
            _ops = ops;
        }

    }
}

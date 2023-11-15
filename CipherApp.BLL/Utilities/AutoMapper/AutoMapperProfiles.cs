using AutoMapper;
using CipherApp.DAL.Entities;
using CipherApp.DAL.Models;
using CipherApp.DTO.Response;

namespace Cipher.BLL.Utilities.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<LoginInputModel, User>();
         
            CreateMap<RegisterInputModel, User>();

            CreateMap<MessageInputModel, Message>();

            CreateMap<User, UserDto>();

            CreateMap<Chat, ChatDto>();

            CreateMap<Message, MessageDto>();
        }
    }
}

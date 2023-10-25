using AutoMapper;
using CipherApp.DAL.Entities;
using CipherApp.DAL.Models;
using CipherApp.DTO.Request;
using CipherApp.DTO.Response;

namespace Cipher.BLL.Utilities.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<Chat, ChatListDto>();
            CreateMap<UserToRegisterDto, User>();

            CreateMap<LoginInputModel, User>();
            CreateMap<RegisterInputModel, User>();

            CreateMap<Message, MessageDto>();
        }
    }
}

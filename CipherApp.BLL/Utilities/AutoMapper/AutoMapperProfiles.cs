using AutoMapper;
using CipherApp.DAL.Entities;
using CipherApp.DTO.Request;
using CipherApp.DTO.Response;

namespace Cipher.BLL.Utilities.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Chat, ChatDto>()
                // Map ChatUser join table to UserDto many-reference
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.ChatUsers.Select(cu => cu.User)));

            CreateMap<Chat, ChatListDto>();

            CreateMap<User, UserDto>()
                // Map ChatUser join table to ChatDto many-reference
                .ForMember(dest => dest.Chats, opt => opt.MapFrom(src => src.ChatUsers.Select(cu => cu.Chat)));

            CreateMap<UserToRegisterDto, User>();

            CreateMap<Message, MessageDto>();
        }
    }
}

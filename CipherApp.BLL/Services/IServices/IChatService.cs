
using CipherApp.DAL.Entities;
using CipherApp.DAL.Models;
using CipherApp.DTO.Response;

namespace CipherApp.BLL.Services.IServices
{
    public interface IChatService
    {
        Task<ChatDto> GetChatAsync(string UID);
        Task<ChatDto> CreateChatAsync(ChatInputModel chatInputModel);
        Task<ChatDto> AddUserAsync(User user, string chatUID);

        Task<ICollection<ChatDto>> GetChatsByUserAsync(string email);

        Task<bool> ChatExistsAsync(string chatUID);

        Task RemoveChatByUserAsync(string email, string UID);
    }
}

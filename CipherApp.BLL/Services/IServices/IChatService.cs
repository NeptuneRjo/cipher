
using CipherApp.DTO.Request;
using CipherApp.DTO.Response;

namespace CipherApp.BLL.Services.IServices
{
    public interface IChatService
    {
        Task<ChatDto> GetChatAsync(int id, string username);

        Task<ChatDto> CreateChatAsync(ChatToCreateDto chatToCreate, int userId);

        Task<ICollection<ChatDto>> GetChatsAsync(int userUID);
    }
}

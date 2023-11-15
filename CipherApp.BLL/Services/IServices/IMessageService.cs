using CipherApp.DAL.Models;
using CipherApp.DTO.Response;

namespace CipherApp.BLL.Services.IServices
{
    public interface IMessageService
    {
        Task<MessageDto> AddMessageAsync(string chatUID, string content, int userId);
    }
}

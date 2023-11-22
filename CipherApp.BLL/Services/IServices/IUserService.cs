using CipherApp.DAL.Entities;
using CipherApp.DTO.Response;

namespace CipherApp.BLL.Services.IServices
{
    public interface IUserService
    {
        Task<User> GetUserAsync(int id);
        Task<User> GetUserAsync(string email);
    }
}

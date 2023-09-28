using CipherApp.DTO.Response;

namespace CipherApp.BLL.Services.IServices
{
    public interface IUserService
    {
        Task<UserDto> GetUserAsync(string uid);
        Task<UserDto> GetUserAsync(int id);
    }
}

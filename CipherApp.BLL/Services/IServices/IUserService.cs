using CipherApp.DTO.Request;
using CipherApp.DTO.Response;

namespace CipherApp.BLL.Services.IServices
{
    public interface IUserService
    {
        Task<UserDto> GetUserAsync(string username);
        Task<UserDto> GetUserAsync(int id);

        Task<UserDto> AuthUserAsync(string username, string password);
        Task<UserDto> AuthUserAsync(int id, string password);

        Task<UserDto> CreateUserAsync(NewUserDto newUser);
    }
}

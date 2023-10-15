using CipherApp.DAL.Entities;
using CipherApp.DTO.Request;
using CipherApp.DTO.Response;

namespace CipherApp.BLL.Services.IServices
{
    public interface IAuthService
    {
        Task<UserDto> LoginAsync(UserToLoginDto userToLoginDto);

        Task<User> RegisterAsync(UserToRegisterDto userToRegisterDto);
    }
}

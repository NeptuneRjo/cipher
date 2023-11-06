using CipherApp.DAL.Entities;
using CipherApp.DAL.Models;
using CipherApp.DTO.Response;

namespace CipherApp.BLL.Services.IServices
{
    public interface IAuthService
    {
        Task<UserDto> LoginAsync(LoginInputModel input);

        Task<UserDto> RegisterAsync(RegisterInputModel input);
    }
}

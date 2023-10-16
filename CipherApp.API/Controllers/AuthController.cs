using Azure.Identity;
using CipherApp.BLL.Services.IServices;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Entities;
using CipherApp.DTO.Request;
using CipherApp.DTO.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CipherApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserToLoginDto userToLogin)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                UserDto user = await _authService.LoginAsync(userToLogin);
                
                return Ok(user);
            }
            catch (NotFoundException)
            {
                return NotFound("A user with these credentials was not found");
            }
            catch (LoginFailedException)
            {
                return BadRequest("Login attempt failed");
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserToRegisterDto userToRegister)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                User user = await _authService.RegisterAsync(userToRegister);

                if (user == null)
                    return StatusCode(500, "Error registering user");

                UserToLoginDto userToLogin = new()
                {
                    username = userToRegister.Username,
                    password = userToRegister.Password,
                };

                UserDto loggedInUser = await _authService.LoginAsync(userToLogin);

                return Ok(loggedInUser);
            }
            catch (UserExistsException)
            {
                return BadRequest("Username already in user");
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}

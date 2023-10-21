using CipherApp.BLL.Services.IServices;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Entities;
using CipherApp.DTO.Request;
using CipherApp.DTO.Response;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace CipherApp.API.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _service;

        public LoginModel(IAuthService service)
        {
            _service = service;
        }

        public class InputModel
        {
            [Required, Display(Name = "Username")]
            public string Username { get; set; }

            [Required, DataType(DataType.Password), Display(Name = "Password")]
            public string Password { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync() 
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                UserDto user = await _service.LoginAsync(MapInput());

                await HandleAuthentication(user.Id, user.Username);

                return RedirectToPage("/Index");
            }
            catch (NotFoundException)
            {
                ModelState.AddModelError("Username", "No user with that username was found");
            }
            catch (LoginFailedException)
            {
                ModelState.AddModelError("Credentials", "Invalid login credentials");
            }

            return Page();
        }

        private UserToLoginDto MapInput()
        {
            UserToLoginDto toLoginDto = new()
            {
                username = Input.Username,
                password = Input.Password,
            };

            return toLoginDto;
        }

        private async Task HandleAuthentication(int userId, string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, username)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60 * 12)
            };

            await HttpContext.SignInAsync(
                "CookieAuthentication",
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}

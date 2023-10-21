using CipherApp.BLL.Services.IServices;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Entities;
using CipherApp.DTO.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace CipherApp.API.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IAuthService _service;

        public RegisterModel(IAuthService service)
        {
            _service = service;
        }

        public class InputModel
        {
            [Required, Display(Name = "Username")]
            public string Username { get; set; }
            
            [Required, DataType(DataType.Password), Display(Name = "Password one")]
            public string Password1 { get; set; }

            [Required, DataType(DataType.Password), Display(Name = "Password two")]
            public string Password2 { get; set; }
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

            if (VerifyPasswords(Input))
            {
                try
                {
                    User user = await _service.RegisterAsync(MapInput(Input));

                    await HandleAuthentication(user.Id, user.Username);

                    return RedirectToPage("/Index");
                }
                catch (UserExistsException)
                {
                    ModelState.AddModelError("Username", "Username already in use");
                }
            }
            ModelState.AddModelError("Password", "Passwords do not match");

            return Page();
        }

        private static bool VerifyPasswords(InputModel input) =>
            input.Password1 == input.Password2;

        private UserToRegisterDto MapInput(InputModel input)
        {
            UserToRegisterDto dto = new()
            {
                Username = Input.Username,
                Password = Input.Password1,
            };

            return dto;
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

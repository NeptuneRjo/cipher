using CipherApp.API.Utilities;
using CipherApp.BLL.Services.IServices;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Models;
using CipherApp.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CipherApp.API.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _service;

        [BindProperty]
        public LoginInputModel LoginInput { get; set; }

        public LoginModel(IAuthService service)
        {
            _service = service;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                UserDto user = await _service.LoginAsync(LoginInput);

                await AuthenticationHandler.Authenticate(
                    HttpContext,
                    user.Id,
                    user.Username,
                    user.Email
                );

                return RedirectToPage("../Index");
            }
            catch (NotFoundException)
            {
                ModelState.AddModelError("LoginError", "Account not found");
            }
            catch (LoginFailedException)
            {
                ModelState.AddModelError("LoginError", "Invalid credentials");
            }
            catch (Exception)
            {
                ModelState.AddModelError("ApplicationError", "Something went wrong");
            }

            return Page();
        }
    }
}

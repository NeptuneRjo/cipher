using CipherApp.API.Utilities;
using CipherApp.BLL.Services.IServices;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Models;
using CipherApp.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CipherApp.API.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly IAuthService _service;

        [BindProperty]
        public RegisterInputModel RegisterInput { get; set; }

        public RegisterModel(IAuthService service)
        {
            _service = service;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                UserDto user = await _service.RegisterAsync(RegisterInput);

                await AuthenticationHandler.Authenticate(
                    HttpContext,
                    user.Id,
                    user.Username,
                    user.Email
                );

                return RedirectToPage("./Index");
            }
            catch (UserExistsException)
            {
                ModelState.AddModelError("RegisterError", "Email already in use");
            }
            catch (Exception)
            {
                ModelState.AddModelError("ApplicationError", "Something went wrong");
            }

            return Page();
        }

        private bool PasswordsValid() =>
            RegisterInput.Password == RegisterInput.ConfirmPassword;
    }
}

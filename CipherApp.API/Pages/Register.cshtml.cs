using CipherApp.API.Utilities;
using CipherApp.BLL.Services.IServices;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Models;
using CipherApp.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CipherApp.API.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IAuthService _service;

        public RegisterModel(IAuthService service)
        {
            _service = service;
        }

        [BindProperty]
        public RegisterInputModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            if (Input.Password == Input.Password2)
            {
                try
                {
                    UserDto user = await _service.RegisterAsync(Input);

                    await AuthenticationHandler
                        .Authenticate(HttpContext, user.Id, user.Username);

                    return RedirectToPage("/Index");
                }
                catch (UserExistsException)
                {
                    ModelState.AddModelError("Username", "Username already in use");
                }
            } else
            {
                ModelState.AddModelError("Password", "Passwords do not match");
            }

            return Page();
        }
    }
}

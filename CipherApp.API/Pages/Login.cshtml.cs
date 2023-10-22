using CipherApp.API.Utilities;
using CipherApp.BLL.Services.IServices;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Models;
using CipherApp.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CipherApp.API.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _service;

        public LoginModel(IAuthService service)
        {
            _service = service;
        }

        [BindProperty]
        public LoginInputModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync() 
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                UserDto user = await _service.LoginAsync(Input);

                await AuthenticationHandler
                    .Authenticate(HttpContext, user.Id, user.Username);

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
    }
}

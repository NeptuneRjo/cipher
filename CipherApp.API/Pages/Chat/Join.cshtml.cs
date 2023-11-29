using CipherApp.BLL.Services.IServices;
using CipherApp.DAL.Entities;
using CipherApp.DAL.Models;
using CipherApp.DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace CipherApp.API.Pages.Chat
{
    [Authorize]
    public class JoinModel : PageModel
    {
        private readonly IChatService _service;

        public JoinModel(IChatService service)
        {
            _service = service;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string UID)
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                string email = User.FindFirst(ClaimTypes.Email)?.Value;
                ChatDto chat = await _service.AddUserAsync(email, UID);

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ModelState.AddModelError("ApplicationError", "Something went wrong");
            }

            return Page();
        }
    }
}

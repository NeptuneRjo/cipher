using CipherApp.BLL.Services.IServices;
using CipherApp.DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace CipherApp.API.Pages.Chat
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IChatService _service;

        public CreateModel(IChatService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                string email = User.FindFirst(ClaimTypes.Email)?.Value;

                ChatDto chat = await _service.CreateChatAsync(email);

                return RedirectToPage("./Index");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

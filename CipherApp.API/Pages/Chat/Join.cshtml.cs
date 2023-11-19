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
        private readonly IChatService _chatService;
        private readonly IUserService _userService;

        public JoinModel(IChatService chatService, IUserService userService)
        {
            _chatService = chatService;
            _userService = userService;
        }

        [BindProperty]
        public JoinInputModel JoinInput { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                string email = User.FindFirst(ClaimTypes.Email)?.Value;
                ChatDto chat = await _chatService.AddUserAsync(email, JoinInput.UID);

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

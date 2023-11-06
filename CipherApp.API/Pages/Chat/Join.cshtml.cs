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
                bool chatExists = await _chatService.ChatExistsAsync(JoinInput.UID);

                if (!chatExists)
                    return NotFound();

                string email = User.FindFirst(ClaimTypes.Email)?.Value;
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                // Redundancies for instances where email might not be available
                User user = email.IsNullOrEmpty()
                    ? await _userService.GetUserAsync(email)
                    : await _userService.GetUserAsync(userId);

                // Add User to chat
                ChatDto chat = await _chatService.AddUserAsync(user, JoinInput.UID);

                return RedirectToPage("./Index", new { id = JoinInput.UID });
            }
            catch (Exception)
            {
                ModelState.AddModelError("ApplicationError", "Something went wrong");
            }

            return Page();
        }
    }
}

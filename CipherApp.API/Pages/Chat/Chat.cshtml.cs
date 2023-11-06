using CipherApp.API.Utilities;
using CipherApp.BLL.Services.IServices;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Entities;
using CipherApp.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CipherApp.API.Pages.Chat
{
    public class ChatModel : PageModel
    {
        private readonly IChatService _chatService;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public ChatModel(
            IChatService service,
            IUserService userService,
            IAuthService authService
        )
        {
            _chatService = service;
            _userService = userService;
            _authService = authService;
        }

        public ChatDto Chat { get; set; }

        public async Task<IActionResult> OnGet(string id)
        {
            try
            {
                // User joins chat (OnGet)
                // New user is generated in db
                //User user = await _userService.GenerateUserAsync();
                // New user is authenticated for user that joined
                //await AuthenticationHandler.Authenticate(
                //    HttpContext, 
                //    user.Id, 
                //    user.Username
                //);
                //// Add User to Chat
                //ChatDto chatDto = await _chatService.AddUserAsync(user, id);
                //// Chat is sent to user
                //Chat = chatDto;

                return Page();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}

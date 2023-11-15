using CipherApp.API.Hubs;
using CipherApp.BLL.Services.IServices;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Models;
using CipherApp.DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace CipherApp.API.Pages.Chat
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IChatService _service;
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly IHubContext<ChatHub> _chatHub;

        public IndexModel(
            IChatService service, 
            IMessageService messageService, 
            IUserService userService,
            IHubContext<ChatHub> chatHub
        )
        {
            _service = service;
            _messageService = messageService;
            _userService = userService;
            _chatHub = chatHub;
        }

        public static ICollection<ChatDto> Chats { get; set; }
        public static ChatDto SelectedChat { get; set; }
        public static ICollection<MessageDto>? Messages { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                string email = User.FindFirst(ClaimTypes.Email)?.Value;

                ICollection<ChatDto> chats = await _service.GetChatsByUserAsync(email);

                Chats = chats;

                return Page();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        public async Task<PartialViewResult> OnGetMessagesPartial(string UID)
        {
            try
            {
                Messages = Chats.First(e => e.UID == UID).Messages;

                return Partial("_MessagesPartial", Messages);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<PartialViewResult> OnPostMessagesPartial(string UID, [FromBody] MessageDto message)
        {
            try
            {
                Messages.Add(message);

                return Partial("_MessagesPartial", Messages);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}

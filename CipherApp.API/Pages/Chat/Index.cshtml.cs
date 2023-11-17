using CipherApp.BLL.Services.IServices;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace CipherApp.API.Pages.Chat
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IChatService _service;

        public IndexModel(IChatService service)
        {
            _service = service;
        }

        public static ICollection<ChatDto> Chats { get; set; }
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

                return Partial("_MessagesPartial", Messages.Reverse().ToList());
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
                // Prevents messages from duplicating due to multiple connections
                if (!Messages.Any(e => e.Id == message.Id))
                {
                    Messages.Add(message);
                }

                return Partial("_MessagesPartial", Messages.Reverse().ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IActionResult> OnGetLeaveChatAsync(string UID)
        {
            try
            {
                string email = User.FindFirst(ClaimTypes.Email)?.Value;

                await _service.RemoveChatByUserAsync(email, UID);

                return Page();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

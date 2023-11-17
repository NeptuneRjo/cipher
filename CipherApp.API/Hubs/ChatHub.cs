using CipherApp.BLL.Services.IServices;
using CipherApp.DAL.Models;
using CipherApp.DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CipherApp.API.Hubs
{
    public interface IChatHub
    {
        Task JoinGroup(string chatUID);

        Task SendMessage(string UID, string content, int userId);

        Task LeaveGroup(string chatUID);
    }

    [Authorize]
    public class ChatHub : Hub, IChatHub
    {
        private readonly IMessageService _msgService;

        public ChatHub(IMessageService msgService)
        {
            _msgService = msgService;
        }

        public async Task SendMessage(string UID, string content, int userID)
        {
            try
            {
                MessageDto message = await _msgService.AddMessageAsync(UID, content, userID);

                await Clients.Group(UID).SendAsync("ReceiveMessage", message);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public async Task JoinGroup(string chatUID) =>
            await Groups.AddToGroupAsync(Context.ConnectionId, chatUID);

        public async Task LeaveGroup(string chatUID) =>
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatUID);

    }
}

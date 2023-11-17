using CipherApp.DAL.Data;
using CipherApp.DAL.Entities;
using CipherApp.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CipherApp.DAL.Repositories
{
    public class ChatRepository : GenericRepository<Chat>, IChatRepository
    {
        private readonly DataContext _context;

        public ChatRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<Chat>> GetChatsByEmail(string email)
        {
            ICollection<Chat> chats = await _context.Chats
                .Include(e => e.Users)
                .Include(e => e.Messages)
                .Where(e => e.Users.Any(user => user.Email == email))
                .ToListAsync();

            return chats;
        }

        public async Task RemoveUserFromChat(string email, string chatUID)
        {
            Chat chat = await _context.Chats
                .Include(chat => chat.Users)
                .FirstAsync(e => e.UID == chatUID);
            
            User user = await _context.Users
                .Include(user => user.Chats)
                .FirstAsync(e => e.Email == email);

            if (user != null && chat != null)
            {
                user.Chats.Remove(chat);
                chat.Users.Remove(user);

                await _context.SaveChangesAsync();
            }
        }
    }
}

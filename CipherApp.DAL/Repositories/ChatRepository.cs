using CipherApp.DAL.Data;
using CipherApp.DAL.Entities;
using CipherApp.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace CipherApp.DAL.Repositories
{
    public class ChatRepository : GenericRepository<Chat>, IChatRepository
    {
        private readonly DataContext _context;

        public ChatRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        private static string GenerateUID()
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder uidBuilder = new StringBuilder();

            for (int i = 0; i < 8; i++)
            {
                int index = random.Next(chars.Length);
                uidBuilder.Append(chars[index]);
            }

            return uidBuilder.ToString();
        }

        public async Task<Chat> AddUserToChat(string email, string chatUID)
        {
            Chat chat = await _context.Chats.Include(chat => chat.Users).FirstAsync(chat => chat.UID == chatUID);
            User user = await _context.Users.Include(user => user.Chats).FirstAsync(user => user.Email == email);

            chat.Users.Add(user);
            user.Chats.Add(chat);

            await _context.SaveChangesAsync();

            return chat;
        }

        public async Task<Chat> CreateChatByEmail(string email)
        {
            User user = await _context.Users.Include(user => user.Chats).FirstAsync(user => user.Email == email);

            Chat newChat = new()
            {
                UID = GenerateUID(),
                Users = new List<User>(),
                Messages = new List<Message>(),
            };

            do
            {
                newChat.UID = GenerateUID();
            } while (await _context.Chats.AnyAsync(chat => chat.UID == newChat.UID));

            user.Chats.Add(newChat);
            newChat.Users.Add(user);

            await _context.SaveChangesAsync();

            return newChat;
        }

        public async Task<Chat> RemoveUserFromChat(string email, string chatUID)
        {
            Chat chat = await _context.Chats
                .Include(chat => chat.Users)
                .FirstOrDefaultAsync(e => e.UID == chatUID);
            
            User user = await _context.Users
                .Include(user => user.Chats)
                .FirstOrDefaultAsync(user => user.Email == email);

            if (user != null && chat != null)
            {
                user.Chats.Remove(chat);
                chat.Users.Remove(user);

                await _context.SaveChangesAsync();
            }
            return chat;
        }
    }
}

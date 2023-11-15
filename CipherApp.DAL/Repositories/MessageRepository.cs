using CipherApp.DAL.Data;
using CipherApp.DAL.Entities;
using CipherApp.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CipherApp.DAL.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        private readonly DataContext _context;

        public MessageRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Message> CreateAndAddToChatAsync(string chatUID, string content, int userId)
        {
            Chat chat = await _context.Chats
                .Include(e => e.Messages)
                    .ThenInclude(msg => msg.User)
                .FirstAsync(e => e.UID == chatUID);

            Message message = new()
            {
                Content = content,
                UserId = userId,
                ChatId = chat.Id,
                CreatedAt = DateTime.Now,
            };

            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();

            return message;
        }
    }
}

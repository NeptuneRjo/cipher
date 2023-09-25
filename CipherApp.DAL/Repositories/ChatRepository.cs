using CipherApp.DAL.Data;
using CipherApp.DAL.Entities;
using CipherApp.DAL.Repositories.IRepositories;

namespace CipherApp.DAL.Repositories
{
    public class ChatRepository : GenericRepository<Chat>, IChatRepository
    {
        private readonly DataContext _context;

        public ChatRepository(DataContext context) : base(context)
        {
            
        }

    }
}

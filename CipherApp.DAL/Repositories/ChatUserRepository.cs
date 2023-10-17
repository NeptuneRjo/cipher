using CipherApp.DAL.Data;
using CipherApp.DAL.Entities;
using CipherApp.DAL.Repositories.IRepositories;

namespace CipherApp.DAL.Repositories
{
    public class ChatUserRepository : GenericRepository<ChatUser>, IChatUserRepository
    {
        private readonly DataContext _context;

        public ChatUserRepository(DataContext context) : base(context)
        {
            _context = context;            
        }

    }
}

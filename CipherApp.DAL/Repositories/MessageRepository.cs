using CipherApp.DAL.Data;
using CipherApp.DAL.Entities;
using CipherApp.DAL.Repositories.IRepositories;

namespace CipherApp.DAL.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        private readonly DataContext _context;

        public MessageRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}

using CipherApp.DAL.Entities;

namespace CipherApp.DAL.Repositories.IRepositories
{
    public interface IChatRepository : IGenericRepository<Chat>
    {
        Task<ICollection<Chat>> GetChatsByEmail(string email);
    }
}

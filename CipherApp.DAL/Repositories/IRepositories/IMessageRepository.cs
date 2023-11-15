using CipherApp.DAL.Entities;

namespace CipherApp.DAL.Repositories.IRepositories
{
    public interface IMessageRepository : IGenericRepository<Message>
    {
        Task<Message> CreateAndAddToChatAsync(string chatUID, string content, int userId);
    }
}

using CipherApp.DAL.Entities;

namespace CipherApp.DAL.Repositories.IRepositories
{
    public interface IChatRepository : IGenericRepository<Chat>
    {
        Task<Chat> RemoveUserFromChat(string email, string chatUID);

        Task<Chat> AddUserToChat(string email, string chatUID);

        Task<Chat> CreateChatByEmail(string email);
    }
}

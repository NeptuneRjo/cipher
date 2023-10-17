namespace CipherApp.BLL.Services.IServices
{
    public interface IChatUserService
    {
        Task CreateChatUserAsync(int chatId, string username);
        
    }
}

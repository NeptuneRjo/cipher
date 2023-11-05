using CipherApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CipherApp.DAL.Data
{
    using BCrypt.Net;

    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                if (context == null) 
                { 
                    throw new ArgumentNullException(nameof(context));
                }

                if (!context.Chats.Any())
                {
                    context.Chats.AddRange(
                        new Chat
                        {
                            UID = "1234ABC",
                            CreatedAt = DateTime.Now,
                            LastMessage = DateTime.Now,
                            Messages = new List<Message>()
                            {
                                new Message()
                                {
                                    Content = "test",
                                    ChatId = 1,
                                },
                                new Message()
                                {
                                    Content = "Hello world",
                                    ChatId = 1
                                }
                            },
                            Users = new List<User>()
                            {
                                new User()
                                {
                                    Email = "test@email.com",
                                    Username = "test",
                                    ChatId = 1,
                                },
                                new User()
                                {
                                    Email = "email@email.com",
                                    Username = "username",
                                    ChatId = 1,
                                }
                            }
                        }
                    );

                    context.SaveChanges();
                }
                    
                return;
            }
        }
    }
}

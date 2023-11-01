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
                                    Username = "user1",
                                    ChatId = 1,
                                },
                                new Message()
                                {
                                    Content = "Hello world",
                                    Username = "user2",
                                    ChatId = 1
                                }
                            },
                            Users = new List<User>()
                            {
                                new User()
                                {
                                    Username = "test",
                                    ChatId = 1,
                                },
                                new User()
                                {
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

        private static void SeedUsers(DataContext context)
        {
            context.Users.AddRange(
                new User
                {
                    Id = 1,
                    Username = "username",
                    Messages = new List<Message>(),
                    ChatId = 1,
                },
                new User
                {
                    Id = 2,
                    Username = "test",
                    Messages = new List<Message>(),
                    ChatId = 2,
                }
            );

            context.SaveChanges();
        }

        private static void SeedChats(DataContext context) 
        {
            
        }

    }
}

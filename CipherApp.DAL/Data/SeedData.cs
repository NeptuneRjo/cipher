using CipherApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CipherApp.DAL.Data
{
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

                if (!context.Users.Any())
                    SeedUsers(context);
                
                if (!context.Chats.Any()) 
                    SeedChats(context);

                return;
            }
        }

        private static void SeedUsers(DataContext context)
        {
            context.Users.AddRange(
                new User
                {
                    Password = "password",
                    Username = "username",
                    Email = "email@email.com",
                    UID = "username:123"
                },
                new User
                {
                    Password = "password",
                    Username = "username",
                    Email = "test@email.com",
                    UID = "username:321"
                }
            );

            context.SaveChanges();
        }

        private static void SeedChats(DataContext context) 
        {
            context.Chats.AddRange(
                new Chat
                {
                    ParticipantOneId = 1,
                    ParticipantTwoId = 2,
                    CreatedAt = DateTime.Now,
                    LastMessage = DateTime.Now,
                    Messages = new List<ChatMessage>()
                    {
                        new ChatMessage
                        {
                            ChatId = 1,
                            SenderId = 1,
                            Content = "Hello world!",
                            SentAt = DateTime.Now,
                        },
                        new ChatMessage
                        {
                            ChatId = 1,
                            SenderId = 2,
                            Content = "test message",
                            SentAt = DateTime.Now,
                        }
                    }
                }
            );
        }

    }
}

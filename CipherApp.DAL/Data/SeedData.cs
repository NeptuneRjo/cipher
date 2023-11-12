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
                            Id = 1,
                            UID = "1234ABC",
                            CreatedAt = DateTime.Now,
                            LastMessage = DateTime.Now,
                            Users = new List<User>()
                            {
                                new User()
                                {
                                    Id = 2,
                                    Email = "test@email.com",
                                    Username = "test",
                                    Password = BCrypt.HashPassword("password"),
                                    Messages = new List<Message>()
                                    {
                                        new Message()
                                        {
                                            ChatId = 1,
                                            UserId = 2,
                                            Content = "Hello World!",
                                            CreatedAt = DateTime.Now,
                                        }
                                    }
                                },
                                new User()
                                {
                                    Id = 1,
                                    Email = "email@email.com",
                                    Username = "username",
                                    Password = BCrypt.HashPassword("password"),
                                    Messages= new List<Message>()
                                    {
                                        new Message()
                                        {
                                            ChatId = 1,
                                            UserId = 1,
                                            Content = "GoodBye!",
                                            CreatedAt = DateTime.Now,
                                        }
                                    },
                                    Chats = new List<Chat>()
                                    {
                                        new Chat()
                                        {
                                            Id = 2,
                                            UID = "ABC1234",
                                            CreatedAt = DateTime.Now,
                                            LastMessage = DateTime.Now,
                                        }

                                    }

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

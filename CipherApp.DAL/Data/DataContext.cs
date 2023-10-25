using CipherApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CipherApp.DAL.Data
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<ChatMessage> ChatMessages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Chat>()
                .HasMany(c => c.Messages)
                .WithOne(m => m.Chat)
                .HasForeignKey(m => m.ChatId);

            builder.Entity<ChatMessage>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId);
        }
    }
}

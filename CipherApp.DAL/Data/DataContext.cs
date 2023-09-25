using CipherApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CipherApp.DAL.Data
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        DbSet<Chat> Chats { get; set; }

        DbSet<Message> Messages { get; set; }

        DbSet<User> Users { get; set; }

        DbSet<ChatUser> ChatUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatUser>()
                .HasKey(cu => new { cu.ChatId, cu.UserId });
        }
    }
}

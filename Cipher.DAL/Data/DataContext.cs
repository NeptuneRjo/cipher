using Microsoft.EntityFrameworkCore;

namespace Cipher.DAL.Data
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
        }
    }
}

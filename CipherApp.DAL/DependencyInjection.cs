using CipherApp.DAL.Data;
using CipherApp.DAL.Repositories;
using CipherApp.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CipherApp.DAL
{
    public static class DependencyInjection
    {
        public static void RegisterDALDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddDbContext<DataContext>(options =>
            {
                bool isDevelopment = Configuration["Development"] == "true";
                var defaultConnectionString = Configuration.GetConnectionString("DefaultConnection");

                if (isDevelopment)
                {
                    options.UseInMemoryDatabase("CipherApp");
                } else
                {
                    options.UseSqlServer(defaultConnectionString);
                }

            });
        }
    }
}

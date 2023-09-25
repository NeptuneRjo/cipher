using CipherApp.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CipherApp.DAL
{
    public static class DependencyInjection
    {
        public static void RegisterDALDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                var defaultConnectionString = Configuration.GetConnectionString("DefaultConnection");

                options.UseSqlServer(defaultConnectionString);
            });
        }
    }
}

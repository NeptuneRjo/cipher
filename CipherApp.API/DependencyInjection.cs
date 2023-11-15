using CipherApp.API.Hubs;

namespace CipherApp.API
{
    public static class DependencyInjection
    {
        public static void RegisterAPIDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IChatHub,  ChatHub>();

            services.AddSignalR();
        }
    }
}

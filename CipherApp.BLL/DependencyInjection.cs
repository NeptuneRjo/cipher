using Cipher.BLL.Utilities.AutoMapper;
using CipherApp.BLL.Services;
using CipherApp.BLL.Services.IServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CipherApp.BLL
{
    public static class DependencyInjection
    {
        public static void RegisterBLLDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMessageService,  MessageService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddAutoMapper(typeof(AutoMapperProfiles));

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
                options.LogoutPath = "/Auth/Signout";
            });

            services.AddLogging(builder =>
            {
                builder.AddConsole();
            });
        }
    }
}

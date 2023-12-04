using AutoMapper;
using CipherApp.BLL;
using CipherApp.BLL.Services.IServices;
using CipherApp.DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace CipherApp.Test.DependencyInjection
{
    public class RegisterBLLDependenciesTests
    {
        private readonly IServiceCollection _services;
        private readonly IConfiguration _config;
        private readonly IServiceProvider _provider;

        public RegisterBLLDependenciesTests()
        {
            _services = new ServiceCollection();
            _config = Substitute.For<IConfiguration>();

            _services.RegisterBLLDependencies(_config);
            _services.RegisterDALDependencies(_config);

            _provider = _services.BuildServiceProvider();
        }

        [Fact]
        public void RegisterBLLDependencies_RegistersServices()
        {
            Assert.NotNull(_provider.GetService<IChatService>());
            Assert.NotNull(_provider.GetService<IUserService>());
            Assert.NotNull(_provider.GetService<IMessageService>());
            Assert.NotNull(_provider.GetService<IAuthService>());
        }

        [Fact]
        public void RegisterBLLDependencies_RegistersAutoMapper()
        {
            Assert.NotNull(_provider.GetService<IMapper>());
        }

        [Fact]
        public void RegisterBLLDependencies_RegistersAuthentication()
        {
            Assert.NotNull(_provider.GetService<IAuthenticationService>());
            Assert.NotNull(_provider.GetService<IAuthenticationService>() is CookieAuthenticationDefaults);
        }

        [Fact]
        public void RegisterBLLDependencies_RegistersLogging()
        {
            Assert.NotNull(_provider.GetService<ILoggerFactory>());
        }
    }
}

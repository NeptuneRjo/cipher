using CipherApp.BLL;
using CipherApp.DAL;
using CipherApp.DAL.Data;
using CipherApp.DAL.Repositories.IRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace CipherApp.Test.DependencyInjection
{
    public class RegisterDALDependenciesTests
    {
        private readonly IServiceCollection _services;
        private readonly IConfiguration _config;
        private readonly IServiceProvider _provider;

        public RegisterDALDependenciesTests()
        {
            _services = new ServiceCollection();
            _config = Substitute.For<IConfiguration>();

            _services.RegisterDALDependencies(_config);
            _services.RegisterBLLDependencies(_config);

            _provider = _services.BuildServiceProvider();
        }

        [Fact]
        public void RegisterDALDependencies_RegistersRepositories()
        {
            Assert.NotNull(_provider.GetService<IChatRepository>());
            Assert.NotNull(_provider.GetService<IMessageRepository>());
            Assert.NotNull(_provider.GetService<IUserRepository>());
        }

        [Fact]
        public void RegisterDALDependencies_RegistersDataContext()
        {
            Assert.NotNull(_provider.GetService<DataContext>());
        }

        [Fact]
        public void RegisterDALDependencies_RegisterDataContext_UsesInMemory()
        {
            _config["Development"] = "true";

            var dataContext = _provider.GetService<DataContext>();
            var databaseType = dataContext.Database.ProviderName;

            Assert.Equal("Microsoft.EntityFrameworkCore.InMemory", databaseType);
            Assert.NotEqual("Microsoft.EntityFrameworkCore.SqlServer", databaseType);
        }

        [Fact]
        public void RegisterDALDependencies_RegisterDataContext_UsesSqlServer()
        {
            _config["Development"] = "false";

            var dataContext = _provider.GetService<DataContext>();
            var databaseType = dataContext.Database.ProviderName;

            Assert.Equal("Microsoft.EntityFrameworkCore.SqlServer", databaseType);
            Assert.NotEqual("Microsoft.EntityFrameworkCore.InMemory", databaseType);
        }
    }
}

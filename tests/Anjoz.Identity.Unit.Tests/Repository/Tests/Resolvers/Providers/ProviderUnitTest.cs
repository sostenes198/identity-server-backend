using Anjoz.Identity.Repository.ConnectionString;
using Anjoz.Identity.Repository.Contratos;
using Anjoz.Identity.Repository.EntityFramework.Resolvers.Providers;
using Anjoz.Identity.Utils.Tests.Fixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Repository.Tests.Resolvers.Providers
{
    public class ProviderUnitTest : IClassFixture<BaseUnitTestFixture>
    {
        private IDatabaseProviderStrategy _providerStrategy;

        private readonly IConnectionStringStrategy _connectionStringStrategy;

        public ProviderUnitTest(BaseUnitTestFixture fixture)
        {
            fixture.AddServices = (services) =>
            {
                services.AddSingleton(provider => fixture.Configuration);
                services.TryAddScoped<IConnectionStringStrategy, ConnectionStringStrategy>();
            };

            _connectionStringStrategy = fixture.ServiceProvider.GetService<IConnectionStringStrategy>();
        }
      
        [Fact]
        public void Deve_Configurar_Provedor_Sql_Server()
        {
            _providerStrategy = new SqlServerDatabaseProviderStrategy(_connectionStringStrategy);

            var dbContextBuilder = new DbContextOptionsBuilder();

            _providerStrategy.ConfigurarProvedor(dbContextBuilder);

            dbContextBuilder.IsConfigured.Should().BeTrue();
        }
        
        [Fact]
        public void Deve_Configurar_Provedor_In_Memory()
        {
            _providerStrategy = new InMemoryDatabaseProviderStrategy();

            var dbContextBuilder = new DbContextOptionsBuilder();

            _providerStrategy.ConfigurarProvedor(dbContextBuilder);

            dbContextBuilder.IsConfigured.Should().BeTrue();
        }
        
        [Fact]
        public void Deve_Configurar_Provedor_SqLite()
        {
            _providerStrategy = new SqLiteDatabaseProviderStrategy();

            var dbContextBuilder = new DbContextOptionsBuilder();

            _providerStrategy.ConfigurarProvedor(dbContextBuilder);

            dbContextBuilder.IsConfigured.Should().BeTrue();
        }
    }
}
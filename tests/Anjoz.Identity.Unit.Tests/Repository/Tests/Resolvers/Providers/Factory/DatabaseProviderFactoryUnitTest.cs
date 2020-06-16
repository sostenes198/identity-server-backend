using System;
using Anjoz.Identity.Domain.Excecoes;
using Anjoz.Identity.Repository.EntityFramework.Resolvers.Providers;
using Anjoz.Identity.Repository.EntityFramework.Resolvers.Providers.Factory;
using Anjoz.Identity.Unit.Tests.Repository.Fixtures;
using FluentAssertions;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Repository.Tests.Resolvers.Providers.Factory
{
    public class DatabaseProviderFactoryUnitTest : IClassFixture<BaseUnitTestRepositoryFixture>
    {
        private readonly BaseUnitTestRepositoryFixture _fixture;
        
        public DatabaseProviderFactoryUnitTest(BaseUnitTestRepositoryFixture fixture)
        {
            _fixture = fixture;
            
        }

        [Theory]
        [InlineData("SQL_SERVER", typeof(SqlServerDatabaseProviderStrategy))]
        [InlineData("IN_MEMORY", typeof(InMemoryDatabaseProviderStrategy))]
        [InlineData("SQLITE", typeof(SqLiteDatabaseProviderStrategy))]
        public void Deve_Retornar_Provedor(string tipoProvider, Type provedorEsperado)
        {
            _fixture.Configuration[DatabaseProviderFactory.DatabaseProvider] = tipoProvider;
            
            var provider = DatabaseProviderFactory.CriarProvedor(_fixture.ServiceProvider, _fixture.Configuration);
            provider.GetType().Should().Be(provedorEsperado);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Provedor_Solicitado_Nao_Existir()
        {
            _fixture.Configuration[DatabaseProviderFactory.DatabaseProvider] = "PROVEDOR_INEXISTENTE";

            Action act = () => DatabaseProviderFactory.CriarProvedor(_fixture.ServiceProvider, _fixture.Configuration);

            act.Should().Throw<BusinessException>()
                .And.Message.Should().Be("Provedor não encontrado.");
        }
    }
}
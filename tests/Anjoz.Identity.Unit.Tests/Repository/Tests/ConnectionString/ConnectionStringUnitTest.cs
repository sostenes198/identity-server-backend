using Anjoz.Identity.Repository.ConnectionString;
using Anjoz.Identity.Repository.Contratos;
using Anjoz.Identity.Unit.Tests.Repository.Fixtures;
using FluentAssertions;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Repository.Tests.ConnectionString
{
    public class ConnectionStringUnitTest : IClassFixture<BaseUnitTestRepositoryFixture>
    {
        private readonly IConnectionStringStrategy _connectionStringStrategy;

        public ConnectionStringUnitTest(BaseUnitTestRepositoryFixture fixture)
        {
            _connectionStringStrategy = new ConnectionStringStrategy(fixture.Configuration);
        }

        [Fact]
        public void Deve_Obter_Connection_String()
        {
            var resultadoEsperado = "Teste";
            var resultado = _connectionStringStrategy.ObterConnectionString();

            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }
    }
}
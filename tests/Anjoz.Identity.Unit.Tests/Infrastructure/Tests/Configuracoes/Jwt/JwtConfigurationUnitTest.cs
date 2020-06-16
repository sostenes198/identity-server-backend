using Anjoz.Identity.Infrastructure;
using Anjoz.Identity.Infrastructure.Configuracoes.Jwt;
using Anjoz.Identity.Utils.Tests.Fixture;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Infrastructure.Tests.Configuracoes.Jwt
{
    public class JwtConfigurationUnitTest : IClassFixture<BaseUnitTestFixture>
    {
        private readonly BaseUnitTestFixture _unitTestFixture;
        
        public JwtConfigurationUnitTest(BaseUnitTestFixture unitTestFixture)
        {
            _unitTestFixture = unitTestFixture;
            
            unitTestFixture.AddServices = (services) =>
                Bootstrap.ConfigurarOptions(services, unitTestFixture.Configuration);
        }
        
        [Fact]
        public void Deve_Obter_Jwt_Configuration()
        {
            var resultadoEsperado = new JwtConfiguration
            {
                Chave = "e4ba2229-3a65-4876-abd6-f990ab97ab5a",
                Duracao = 1
            };
            
            var jwtConfiguration = _unitTestFixture.ServiceProvider.GetService<IOptions<JwtConfiguration>>();
            
            jwtConfiguration.Value.Should().BeEquivalentTo(resultadoEsperado);
        }
    }
}
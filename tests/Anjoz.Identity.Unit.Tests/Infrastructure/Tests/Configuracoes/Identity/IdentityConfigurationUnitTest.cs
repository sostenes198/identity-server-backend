using Anjoz.Identity.Infrastructure;
using Anjoz.Identity.Infrastructure.Configuracoes.Identity;
using Anjoz.Identity.Utils.Tests.Fixture;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Infrastructure.Tests.Configuracoes.Identity
{
    public class IdentityConfigurationUnitTest : IClassFixture<BaseUnitTestFixture>
    {
        private readonly BaseUnitTestFixture _unitTestFixture;
        
        public IdentityConfigurationUnitTest(BaseUnitTestFixture unitTestFixture)
        {
            _unitTestFixture = unitTestFixture;
            
            unitTestFixture.AddServices = (services) =>
                Bootstrap.ConfigurarOptions(services, unitTestFixture.Configuration);
            
        }
        [Fact]
        public void Deve_Obter_Identity_Configuration()
        {
            var resultadoEsperado = new IdentityConfiguration
            {
                RequerConfirmacaoEmail = false,
                LockoutConfiguracao = new IdentityConfigurationLockout
                {
                    PermitirParaNovosUsuarios = true,
                    TentativaMaximaErrors = 5
                },
                SenhaConfiguracao = new IdentityConfigurationPassword
                {
                    QuantidadeCaracterMinimo = 5,
                    RequerDigito = true,
                    RequerLetraMinuscula = true,
                    RequerLetraMaiuscula = true,
                    QuantidadeCaracterUnico = 1,
                    RequererCaracterEspecial = false
                }
            };
            var identityConfiguration = _unitTestFixture.ServiceProvider.GetService<IOptions<IdentityConfiguration>>();
            
            identityConfiguration.Value.Should().BeEquivalentTo(resultadoEsperado);
        }
    }
}
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Servicos.Identity;
using Anjoz.Identity.Unit.Tests.Domain.Fixtures.Identity;
using FluentAssertions;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Domain.Tests.Servicos.Identity
{
    public class ClaimServiceUnitTest : IClassFixture<ClaimServiceFixture>
    {
        private readonly IClaimService _claimService;

        public ClaimServiceUnitTest(ClaimServiceFixture fixture)
        {
            _claimService = new ClaimService(fixture.InicializarClaimRepository(),
                fixture.DomainValidator<Claim>());
        }

        [Fact]
        public async Task Deve_Criar_Claim()
        {
            var claim = new Claim {Valor = "teste", Descricao = "Teste"};

            var resultadoEsperado = new Claim {Valor = "TESTE", ValorNormalizado = "TESTE", Descricao = "Teste"};

            await _claimService.CriarAsync(claim);
            
            claim.Should().BeEquivalentTo(resultadoEsperado);
        }
    }
}
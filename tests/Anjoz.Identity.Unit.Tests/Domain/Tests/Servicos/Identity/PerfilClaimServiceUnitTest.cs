using System.Linq;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Servicos.Identity;
using Anjoz.Identity.Unit.Tests.Domain.Fixtures.Identity;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using FluentAssertions;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Domain.Tests.Servicos.Identity
{
    public class PerfilClaimServiceUnitTest : IClassFixture<PerfilClaimFixture>
    {
        private readonly PerfilClaimService _perfilClaimService;

        public PerfilClaimServiceUnitTest(PerfilClaimFixture fixture)
        {
            _perfilClaimService = new PerfilClaimService(fixture.InicializarRepositorioPerfilClaim(),
                fixture.InicializarClaimService(), fixture.DomainValidator<PerfilClaim>());
        }

        [Fact]
        public void Deve_Popular_Vinculo_Perfil_Claims()
        {
            var resultadoEsperado = PerfilClaimUtils.PerfilClaims;
            var resultado = _perfilClaimService.PopularVinculos(1, new[] {1, 2, 3, 4, 5});

            resultado.Should().BeEquivalentTo(resultadoEsperado, opt => opt.Excluding(prop => prop.Claim));
        }

        [Fact]
        public async Task Deve_Listar_Todas_Claim_Perfil()
        {
            var resultadoEsperado = PerfilClaimUtils.PerfilClaims;
            var resultado = await _perfilClaimService.ListarTodosVinculosEntidade(1);

            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task Deve_Listar_Todas_Claims()
        {
            var resultadoEsperado = ClaimUtils.Claims.Where(lnq => new[] {1, 2, 3, 4, 5}.Contains(lnq.Id)).Select(lnq => lnq.Id);
            var resultado = await _perfilClaimService.ListarTodosVinculos(new[] {1, 2, 3, 4, 5});

            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }
    }
}
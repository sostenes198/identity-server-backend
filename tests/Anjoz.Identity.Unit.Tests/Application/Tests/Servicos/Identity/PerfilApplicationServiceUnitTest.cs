using System.Linq;
using System.Threading.Tasks;
using Anjoz.Identity.Application.Contratos.Identity;
using Anjoz.Identity.Application.Dtos.Base;
using Anjoz.Identity.Application.Dtos.Identity.Claim;
using Anjoz.Identity.Application.Dtos.Identity.Perfil;
using Anjoz.Identity.Application.Dtos.Paginacao;
using Anjoz.Identity.Application.Servicos.Identity;
using Anjoz.Identity.Unit.Tests.Application.Fixture.Crud.Identity;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using FluentAssertions;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Application.Tests.Servicos.Identity
{
    public class PerfilApplicationServiceUnitTest : IClassFixture<PerfilApplicationFixture>
    {
        private readonly IPerfilApplicationService _perfilApplicationService;

        public PerfilApplicationServiceUnitTest(PerfilApplicationFixture fixture)
        {
            _perfilApplicationService = new PerfilApplicationService(fixture.InicializarPerfilService(), fixture.Mapper, null);
        }

        [Fact]
        public async Task Deve_Obter_Por_Nome()
        {
            var resultadoEsperado = new PerfilDto {Id = 9998, Nome = PerfilUtils.NomePerfilInvalido};

            var resultado = await _perfilApplicationService.ObterPorNomeAsync(PerfilUtils.NomePerfilInvalido);

            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task Deve_Listar_Claims()
        {
            var resultado = await _perfilApplicationService.ListarClaims(1);
            var claimDtos = ClaimUtils.ClaimsDto.Where(lnq => resultado.Items.Select(t => t.Id).Contains(lnq.Id));
            var resultadoEsperado = new PagedListDto<ClaimDto>(claimDtos.ToList(), new PageInfoDto {PageNumber = 1, PageSize = 5, TotalPages = 1});
            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }
    }
}
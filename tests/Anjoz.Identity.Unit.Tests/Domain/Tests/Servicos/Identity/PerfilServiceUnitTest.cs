using System.Linq;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Excecoes;
using Anjoz.Identity.Domain.Servicos.Identity;
using Anjoz.Identity.Unit.Tests.Domain.Fixtures.Identity;
using Anjoz.Identity.Utils.Tests.Fakers.Identity;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using FluentAssertions;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Domain.Tests.Servicos.Identity
{
    public class PerfilServiceUnitTest : IClassFixture<PerfilServiceFixture>
    {
        private readonly IPerfilService _perfilService;

        public PerfilServiceUnitTest(PerfilServiceFixture fixture)
        {
            _perfilService = new PerfilService(fixture.InicializarPerfilRepository(),
                fixture.InicializarPerfilClaimVinculoService());
        }

        [Fact]
        public void Deve_Cadastrar_Perfil()
        {
            var perfil = new PerfilFaker().Generate();
            perfil.PerfisClaims = PerfilClaimUtils.PerfilClaims;

            _perfilService.Invoking(lnq => lnq.CriarAsync(perfil))
                .Should()
                .NotThrow();
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Cadastrar_Perfil_E_Perfil_Invalido()
        {
            var perfil = new Perfil {Name = PerfilUtils.NomePerfilInvalido};

            _perfilService.Invoking(lnq => lnq.CriarAsync(perfil))
                .Should()
                .Throw<BusinessException>();
        }

        [Fact]
        public void Deve_Atualizar_Perfil()
        {
            var perfil = new Perfil {Id = 1, Name = "TesteAtualizacao", PerfisClaims = PerfilClaimUtils.PerfilClaims};

            _perfilService.Invoking(lnq => lnq.AtualizarAsync(perfil))
                .Should()
                .NotThrow();
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Atualizar_Perfil_E_Perfil_Invalido()
        {
            var perfil = new Perfil {Id = 1, Name = PerfilUtils.NomePerfilInvalido};
            _perfilService.Invoking(lnq => lnq.AtualizarAsync(perfil))
                .Should()
                .Throw<BusinessException>();
        }

        [Fact]
        public void Deve_Excluir_Perfil()
        {
            _perfilService.Invoking(lnq => lnq.ExcluirAsync(1))
                .Should()
                .NotThrow();
        }

        [Fact]
        public void Deve_Lancar_Execao_Quando_Excluir_Perfil_E_Perfil_Nao_Existir()
        {
            _perfilService.Invoking(lnq => lnq.ExcluirAsync(9999))
                .Should()
                .Throw<NotFoundException>();
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Excluir_Perfil_E_Perfil_Invalido()
        {
            _perfilService.Invoking(lnq => lnq.ExcluirAsync(9998))
                .Should()
                .Throw<BusinessException>();
        }

        [Fact]
        public async Task Deve_Obter_Perfil_Por_Id()
        {
            var resultadoEsperado = PerfilUtils.Perfis.First(lnq => lnq.Id == 1);

            var resultado = await _perfilService.ObterPorIdAsync(1);

            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task Deve_Obter_Perfil_Por_Nome()
        {
            var resultadoEsperado = PerfilUtils.Perfis.First(lnq => lnq.NormalizedName == PerfilUtils.NomePerfilInvalido.ToUpper());

            var resultado = await _perfilService.ObterPorNomeAsync(PerfilUtils.NomePerfilInvalido);

            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task Deve_Obter_Todos_Perfis()
        {
            var resultadoEsperado = PerfilUtils.Perfis;

            var resultado = await _perfilService.ListarPorAsync();

            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task Deve_Obter_Perfis_Com_Filtro()
        {
            var resultadoEsperado = PerfilUtils.Perfis.Where(lnq => lnq.NormalizedName.Contains("CC")).ToList();

            var resultado = await _perfilService.ListarPorAsync(lnq => lnq.NormalizedName.Contains("CC"));

            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task Deve_Listar_Claims()
        {
            var resultado = await _perfilService.ListarClaims(1);
            var resultadoEsperado = PerfilClaimUtils.PerfilClaims.Where(lnq => lnq.RoleId == 1).Select(lnq => lnq.Claim);
            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }
    }
}
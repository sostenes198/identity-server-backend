using System.Linq;
using System.Threading.Tasks;
using Anjoz.Identity.Application.Contratos.Identity;
using Anjoz.Identity.Application.Dtos.Base;
using Anjoz.Identity.Application.Dtos.Identity.Claim;
using Anjoz.Identity.Application.Dtos.Identity.Usuario;
using Anjoz.Identity.Application.Dtos.Identity.Usuario.AlteracaoSenha;
using Anjoz.Identity.Application.Dtos.Paginacao;
using Anjoz.Identity.Application.Filtros.Identity;
using Anjoz.Identity.Application.Servicos.Identity;
using Anjoz.Identity.Domain.Recursos;
using Anjoz.Identity.Unit.Tests.Application.Fixture.Crud.Identity;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using FluentAssertions;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Application.Tests.Servicos.Identity
{
    public class UsuarioApplicationServiceUnitTest : IClassFixture<UsuarioApplicationFixture>
    {
        private readonly IUsuarioApplicationService _usuarioApplicationService;

        public UsuarioApplicationServiceUnitTest(UsuarioApplicationFixture fixture)
        {
            _usuarioApplicationService = new UsuarioApplicationService(fixture.InicializarUsuarioService(), fixture.Mapper, new UsuarioGeradorFiltro());
        }

        [Fact]
        public async Task Deve_Obter_Por_Nome()
        {
            var resultadoEsperado = new UsuarioDto {Id = 1, CodigoEquipe = 1, Email = "teste1@teste.com", Nome = UsuarioUtils.NomeUsuarioTeste, Login = UsuarioUtils.NomeUsuarioTeste, Telefone = "111111"};

            var resultado = await _usuarioApplicationService.ObterPorNomeAsync(UsuarioUtils.NomeUsuarioTeste);

            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task Deve_Listar_Claims()
        {
            var resultado = await _usuarioApplicationService.ListarClaims(1);
            var claimDtos = ClaimUtils.ClaimsDto.Where(lnq => resultado.Items.Select(t => t.Id).Contains(lnq.Id));
            var resultadoEsperado = new PagedListDto<ClaimDto>(claimDtos.ToList(), new PageInfoDto {PageNumber = 1, PageSize = 5, TotalPages = 1});
            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task Deve_Aterar_Senha()
        {
            var usuarioAlteracaoSenha = new UsuarioAlteracaoSenhaDto
            {
                Id = 1,
                SenhaAtual = "Abcc%13",
                NovaSenha = "ABc321@",
                ConfirmacaoSenha = "ABc321@"
            };
            var resultadoEsperado = new UsuarioAlteracaoSenhaResultadoDto
            {
                Mensagem = Mensagens.Usuario_SenhaAlteradaComSucesso
            };
            var resultado = await _usuarioApplicationService.AlterarSenha(usuarioAlteracaoSenha);
            resultado.Mensagem.Should().BeEquivalentTo(resultadoEsperado.Mensagem);
        }
    }
}
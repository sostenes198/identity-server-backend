using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anjoz.Identity.Application.Dtos.Base;
using Anjoz.Identity.Application.Dtos.Identity.Claim;
using Anjoz.Identity.Application.Dtos.Identity.Usuario;
using Anjoz.Identity.Application.Dtos.Identity.Usuario.AlteracaoSenha;
using Anjoz.Identity.Application.Dtos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Entidades.Paginacao;
using Anjoz.Identity.Domain.Recursos;
using Anjoz.Identity.Integration.Tests.Extensions;
using Anjoz.Identity.Integration.Tests.Fixtures.Identity;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Anjoz.Identity.Integration.Tests.Tests.Identity
{
    public class UsuariosControllerTests : IClassFixture<UsuariosControllerFixture>
    {
        private readonly UsuariosControllerFixture _fixture;
        private readonly string _baseUrlUsuario;
        private readonly IUsuarioService _usuarioService;
        private readonly IPerfilService _perfilService;
        private readonly IClaimService _claimService;
        private readonly IUsuarioClaimService _usuarioClaimVinculoService;
        private readonly IUsuarioPerfilService _usuarioPerfilVinculoService;


        public UsuariosControllerTests(UsuariosControllerFixture fixture)
        {
            _fixture = fixture;
            _baseUrlUsuario = $"{fixture.BaseUrl}/usuarios";
            _usuarioService = fixture.ServiceProvider.GetService<IUsuarioService>();
            _perfilService = fixture.ServiceProvider.GetService<IPerfilService>();
            _claimService = fixture.ServiceProvider.GetService<IClaimService>();
            _usuarioClaimVinculoService = fixture.ServiceProvider.GetService<IUsuarioClaimService>();
            _usuarioPerfilVinculoService = fixture.ServiceProvider.GetService<IUsuarioPerfilService>();
        }

        [Fact]
        public Task Deve_Listar_Todos_Usuarios()
        {
            var usuarios = _fixture.UsuariosParaListar;

            return _fixture.ListarTodosRegistrosAsync<Usuario, UsuarioDto>(
                _baseUrlUsuario,
                _fixture.CriarRegistros(usuarios, _usuarioService),
                async () => await _usuarioService.ListarPorAsync()
            );
        }

        [Fact]
        public Task Deve_Listar_Usuarios_Com_Filtro()
        {
            var usuarios = _fixture.UsuariosParaListarComFiltro;

            var usuarioFiltroDto = new PageListFiltroDto<UsuarioFiltroDto> {Filtro = new UsuarioFiltroDto {Nome = "AATest"}, PagedParam = new PagedParamFiltroDto {PageNumber = 1, PageSize = 10}};

            return _fixture.ListarTodosRegistrosComFiltroAsync<Usuario, PageListFiltroDto<UsuarioFiltroDto>, UsuarioDto>(
                _baseUrlUsuario,
                usuarioFiltroDto,
                _fixture.CriarRegistros(usuarios, _usuarioService),
                async () => await _usuarioService.ListarPorAsync(lnq => lnq.UserName.Contains("AATest"), default, new PagedParam(10, 1))
            );
        }

        [Fact]
        public Task Deve_Listar_Usuario_Por_Id()
        {
            var usuario = new Usuario {UserName = "Teste5", Login = "Teste5", PasswordHash = "Abc321@@", Email = "teste5@teste.com"};

            return _fixture.ObterRegistroAsync<Usuario, UsuarioDto>(
                _fixture.CriarRegistros(usuario, _usuarioService),
                () => $"{_baseUrlUsuario}/{usuario.Id}",
                (response) => _usuarioService.ObterPorIdAsync(response.Id)
            );
        }

        [Fact]
        public Task Deve_Listar_Usuario_Por_Nome()
        {
            var usuario = new Usuario {UserName = "Teste6", Login = "Teste6", PasswordHash = "Abc321@@", Email = "teste6@teste.com"};
            return _fixture.ObterRegistroAsync<Usuario, UsuarioDto>(
                _fixture.CriarRegistros(usuario, _usuarioService),
                () => $"{_baseUrlUsuario}/nome/{usuario.UserName}",
                (response) => _usuarioService.ObterPorIdAsync(response.Id)
            );
        }

        [Fact]
        public Task Deve_Listar_Claims()
        {
            var claims = _fixture.ClaimsUsuario;

            _fixture.GerarRegistros(claims, _claimService);

            var claimsId = new HashSet<int>(claims.Select(lnq => lnq.Id).ToArray());

            var usuario = new Usuario
            {
                UserName = "TesteLisarClaims", PasswordHash = "Abc321@@", Email = "teste6@teste.com", Login = "TesteLisarClaims"
            };
            var usuarioClaims = new List<UsuarioClaim>();
            foreach (var claimId in claimsId)
            {
                usuarioClaims.Add(new UsuarioClaim {ClaimId = claimId});
            }

            usuario.UsuariosClaims = usuarioClaims;

            return _fixture.ObterRegistroAsync<IPagedList<Claim>, PagedListDto<ClaimDto>>(
                _fixture.CriarRegistros(usuario, _usuarioService),
                () => $"{_baseUrlUsuario}/{usuario.Id}/claims",
                async (response) => await _claimService.ListarPorAsync(lnq => response.Items.Select(t => t.Id).Contains(lnq.Id)));
        }

        [Fact]
        public async Task Deve_Criar_Usuario()
        {
            var claims = _fixture.Claims;
            var perfis = _fixture.Perfis;

            _fixture.GerarRegistros(claims, _claimService);
            _fixture.GerarRegistros(perfis, _perfilService);

            var claimsId = new HashSet<int>(claims.Select(lnq => lnq.Id).ToArray());
            var perfisId = new HashSet<int>(perfis.Select(lnq => lnq.Id).ToArray());

            var usuarioCriarDto = new UsuarioCriarDto
            {
                Nome = "Teste7", Senha = "Abc321@@", Email = "teste6@teste.com", Login = "Teste7", CodigoEquipe = 2,
                ClaimsId = claimsId,
                PerfisId = perfisId
            };

            await _fixture.CriarAsync<Usuario, UsuarioCriarDto, UsuarioDto>(
                _baseUrlUsuario,
                usuarioCriarDto,
                (response) => _usuarioService.ObterPorIdAsync(response.Id)
            );

            var usuario = await _usuarioService.ObterPorNomeAsync(usuarioCriarDto.Nome);
            var claimsIdUsuario = (await _usuarioClaimVinculoService.ListarPorAsync(lnq => lnq.UserId == usuario.Id)).Select(lnq => lnq.ClaimId);
            var perfisIdUsuario = (await _usuarioPerfilVinculoService.ListarPorAsync(lnq => lnq.UserId == usuario.Id)).Select(lnq => lnq.RoleId);

            claimsId.Should().BeEquivalentTo(claimsIdUsuario);
            perfisId.Should().BeEquivalentTo(perfisIdUsuario);
        }

        [Fact]
        public async Task Deve_Atualizar_Usuario()
        {
            var claims = _fixture.ClaimsAtualizar;
            var perfis = _fixture.PerfisAtualizar;

            _fixture.GerarRegistros(claims, _claimService);
            _fixture.GerarRegistros(perfis, _perfilService);

            var claimsId = new HashSet<int>(claims.Select(lnq => lnq.Id).ToArray());
            var perfisId = new HashSet<int>(perfis.Select(lnq => lnq.Id).ToArray());

            var usuario = new Usuario {UserName = "Teste7", CodigoEquipe = 1, PasswordHash = "Abc321@@", Email = "teste7@teste.com", Login = "Teste7"};

            await _fixture.AtualizarAsync<Usuario, UsuarioAtualizarDto, UsuarioDto>(
                _baseUrlUsuario,
                _fixture.CriarRegistros(usuario, _usuarioService),
                () => new UsuarioAtualizarDto
                {
                    Id = usuario.Id, Nome = "TESTANDO", Email = "TESTANDO@teste.com", Login = "TESTANDO", CodigoEquipe = 2,
                    ClaimsId = claimsId,
                    PerfisId = perfisId
                },
                (response) => _usuarioService.ObterPorIdAsync(response.Id)
            );

            var claimsIdUsuario = (await _usuarioClaimVinculoService.ListarPorAsync(lnq => lnq.UserId == usuario.Id)).Select(lnq => lnq.ClaimId);
            var perfisIdUsuario = (await _usuarioPerfilVinculoService.ListarPorAsync(lnq => lnq.UserId == usuario.Id)).Select(lnq => lnq.RoleId);

            claimsId.Should().BeEquivalentTo(claimsIdUsuario);
            perfisId.Should().BeEquivalentTo(perfisIdUsuario);
        }

        [Fact]
        public Task Deve_Excluir_Usuario()
        {
            var usuario = new Usuario {UserName = "Teste8", PasswordHash = "Abc321@@", Email = "teste7@teste.com", Login = "Teste8"};

            return _fixture.ExcluirAsync<Usuario, int>(
                _fixture.CriarRegistros(usuario, _usuarioService),
                () => $"{_baseUrlUsuario}/{usuario.Id}",
                (response) => _usuarioService.ObterPorIdAsync(response)
            );
        }

        [Fact]
        public async Task Deve_Alterar_Senha_Usuario()
        {
            var usuario = new Usuario {UserName = "Teste9", PasswordHash = "Abc321@@", Email = "teste9@teste.com", Login = "Teste9"};
            _fixture.CriarRegistros(usuario, _usuarioService)();

            var resultadoEsperado = new UsuarioAlteracaoSenhaResultadoDto {Mensagem = Mensagens.Usuario_SenhaAlteradaComSucesso};

            var resultado = await BaseWebApiExtensions.EnviarAsync<UsuarioAlteracaoSenhaResultadoDto, UsuarioAlteracaoSenhaDto>(
                $"{_baseUrlUsuario}/senha",
                new UsuarioAlteracaoSenhaDto
                {
                    Id = usuario.Id,
                    SenhaAtual = "Abc321@@",
                    NovaSenha = "ABa123@@",
                    ConfirmacaoSenha = "ABa123@@"
                },
                _fixture.Client.PutAsync, StatusCodes.Status200OK);
            
            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }
    }
}
using System.Linq;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Excecoes;
using Anjoz.Identity.Domain.Recursos;
using Anjoz.Identity.Domain.VO;
using Anjoz.Identity.Unit.Tests.Domain.Fixtures.Identity;
using Anjoz.Identity.Utils.Tests.Extensoes;
using Anjoz.Identity.Utils.Tests.Fakers.Identity;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using FluentAssertions;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Domain.Tests.Servicos.Identity
{
    public class UsuarioServiceUnitTest : IClassFixture<UsuarioServiceFixture>
    {
        private readonly UsuarioServiceFixture _fixture;
        private readonly IUsuarioService _usuarioService;

        public UsuarioServiceUnitTest(UsuarioServiceFixture fixture)
        {
            _fixture = fixture;
            _usuarioService = fixture.InicializarUsuarioService();
        }

        [Fact]
        public async Task Deve_Obter_Por_Id()
        {
            var usuarioEsperado = UsuarioUtils.Usuarios.Single(lnq => lnq.Id == 1);
            var usuario = await _usuarioService.ObterPorIdAsync(1);
            usuario.Should().BeEquivalentTo(usuarioEsperado, UsuarioUtils.PropriedadesParaIgnorar);
        }

        [Fact]
        public async Task Deve_Retornar_Nullo_Quando_Obter_Usuario_Por_Id_Nao_Encontrado()
        {
            var usuario = await _usuarioService.ObterPorIdAsync(999);
            usuario.Should().BeNull();
        }

        [Fact]
        public async Task Deve_Obter_Usuario_Por_Nome()
        {
            var usuarioEsperado = UsuarioUtils.Usuarios.Single(lnq => lnq.NormalizedUserName == "TESTE1");
            var usuario = await _usuarioService.ObterPorNomeAsync("TESTE1");
            usuario.Should().BeEquivalentTo(usuarioEsperado, UsuarioUtils.PropriedadesParaIgnorar);
        }

        [Fact]
        public async Task Deve_Obter_Usuario_Por_Login()
        {
            var usuarioEsperado = UsuarioUtils.Usuarios.Single(lnq => lnq.LoginNormalizado == "TESTE2");
            var usuario = await _usuarioService.ObterPorLoginAsync("TESTE2");
            usuario.Should().BeEquivalentTo(usuarioEsperado, UsuarioUtils.PropriedadesParaIgnorar);
        }

        [Fact]
        public async Task Deve_Listar_Todos()
        {
            var usuariosEsperado = UsuarioUtils.Usuarios;
            var usuarios = await _usuarioService.ListarPorAsync();

            usuarios.Should().BeEquivalentTo(usuariosEsperado, UsuarioUtils.PropriedadesParaIgnorar);
        }

        [Fact]
        public async Task Deve_Listar_Todos_Usuarios_Aonde_Id_Esteja_Entre_1_a_5()
        {
            var filtro = new[] {1, 2, 3, 4, 5};
            var usuariosEsperados = UsuarioUtils.Usuarios.Where(lnq => filtro.Contains(lnq.Id)).ToList();
            var usuarios = await _usuarioService.ListarPorAsync(lnq => filtro.Contains(lnq.Id));

            usuarios.Should().BeEquivalentTo(usuariosEsperados, UsuarioUtils.PropriedadesParaIgnorar);
        }

        [Fact]
        public async Task Deve_Retornar_Nullo_Quando_Usuarios_Filtrados_Nao_Existirem()
        {
            var usuarios = await _usuarioService.ListarPorAsync(lnq => new[] {996, 997, 998, 999}.Contains(lnq.Id));

            usuarios.Should().BeEmpty();
        }

        [Fact]
        public void Deve_Criar()
        {
            var usuario = new UsuarioFaker().Generate();
            usuario.PasswordHash = "ab@123AB";
            usuario.UsuariosClaims = UsuarioClaimUtils.UsuariosClaims;
            usuario.UsuariosPerfis = UsuarioPerfilUtils.UsuariosPerfis;

            _usuarioService.Invoking(async lnq => await lnq.CriarAsync(usuario))
                .Should()
                .NotThrow();
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Usuario_Nao_For_Perssistido_Com_Sucesso()
        {
            var servico = _fixture.InicializarUsuarioService(false);
            var usuario = new UsuarioFaker().Generate();
            usuario.PasswordHash = "ab#c@A";


            servico.Invoking(async lnq => await lnq.CriarAsync(usuario))
                .Should()
                .Throw<BusinessException>();
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Usuario_Invalido()
        {
            var servico = _fixture.InicializarUsuarioService(false, false);
            var usuario = new UsuarioFaker().Generate();
            usuario.PasswordHash = "ab@123AB";

            servico.Invoking(async lnq => await lnq.CriarAsync(usuario))
                .Should()
                .ValidarExcecaoPropriedade();
        }

        [Fact]
        public void Deve_Atualizar()
        {
            var usuario = new UsuarioFaker().Generate();
            usuario.Id = 1;
            usuario.UserName = "UpdateTeste";
            usuario.Email = "update@teste.com";
            usuario.PasswordHash = "a12Bc#2D";
            usuario.PhoneNumber = "1257410";
            usuario.UsuariosClaims = UsuarioClaimUtils.UsuariosClaims;

            _usuarioService.Invoking(async lnq => await lnq.AtualizarAsync(usuario))
                .Should()
                .NotThrow();
        }

        [Fact]
        public void Deve_Atualizar_Sem_Claims()
        {
            var usuario = new UsuarioFaker().Generate();
            usuario.Id = 1;
            usuario.UserName = "UpdateTeste";
            usuario.Email = "update@teste.com";
            usuario.PasswordHash = "a12Bc#2D";
            usuario.PhoneNumber = "1257410";
            usuario.UsuariosClaims = UsuarioClaimUtils.UsuariosClaims;
            usuario.UsuariosPerfis = UsuarioPerfilUtils.UsuariosPerfis;

            _usuarioService.Invoking(async lnq => await lnq.AtualizarAsync(usuario))
                .Should()
                .NotThrow();
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Atualizar_Usuario_E_Usuario_Nao_Existir()
        {
            var usuario = new UsuarioFaker().Generate();
            usuario.Id = 99;

            _usuarioService.Invoking(async lnq => await lnq.AtualizarAsync(usuario))
                .Should()
                .ValidarExcecaoPropriedade(mensagem: "Usuário não encontrado.");
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Atualizar_Usuario_E_Usuario_Estiver_Invalido()
        {
            var servico = _fixture.InicializarUsuarioService(false, updateValidator: false);
            var usuario = new UsuarioFaker().Generate();
            usuario.Id = 99;

            servico.Invoking(async lnq => await lnq.CriarAsync(usuario))
                .Should()
                .ValidarExcecaoPropriedade();
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Atualizar_Usuario_E_Usuario_Nao_For_Perssistido_Com_Sucesso()
        {
            var servico = _fixture.InicializarUsuarioService(false);
            var usuario = new UsuarioFaker().Generate();
            usuario.Id = 1;


            servico.Invoking(async lnq => await lnq.AtualizarAsync(usuario))
                .Should()
                .ValidarExcecaoPropriedade();
        }

        [Fact]
        public void Deve_Deletar()
        {
            _usuarioService.Invoking(async lnq => await lnq.ExcluirAsync(1))
                .Should()
                .NotThrow();
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Deletar_Usuario_E_Usuario_Nao_Existir()
        {
            _usuarioService.Invoking(async lnq => await lnq.ExcluirAsync(999))
                .Should()
                .ValidarExcecaoPropriedade(mensagem: "Usuário não encontrado.");
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Deletar_Usuario_E_Usuario_Nao_For_Deletado_Com_Sucesso()
        {
            var servico = _fixture.InicializarUsuarioService(false);

            servico.Invoking(async lnq => await lnq.ExcluirAsync(1))
                .Should()
                .ValidarExcecaoPropriedade();
        }

        [Fact]
        public async Task Deve_Listar_Todas_Claims()
        {
            var resultado = await _usuarioService.ListarClaims(1);
            var resultadoEsperado = UsuarioClaimUtils.UsuariosClaims.Where(lnq => lnq.UserId == 1).Select(lnq => lnq.Claim);
            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public void Deve_Alterar_Senha_Usuario()
        {
            var usuarioAlteracaoSenhaVo = new UsuarioAlteracaoSenhaVo
            {
                Id = 1,
                SenhaAtual = "BBa321@%",
                NovaSenha = "Abc123@%",
                ConfirmacaoSenha = "Abc123@%"
            };
            _usuarioService.Invoking(async lnq => await lnq.AlterarSenha(usuarioAlteracaoSenhaVo))
                .Should()
                .NotThrow();
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Alterar_Senha_E_Senha_Atual_Informada_For_Invalida()
        {
            var usuarioAlteracaoSenhaVo = new UsuarioAlteracaoSenhaVo
            {
                Id = 1,
                SenhaAtual = "BBa321@%",
                NovaSenha = "Abc123@%",
                ConfirmacaoSenha = "Abc123@%"
            };
            var service = _fixture.InicializarUsuarioService(senhaValida: false);
            service.Invoking(async lnq => await lnq.AlterarSenha(usuarioAlteracaoSenhaVo))
                .Should()
                .Throw<BusinessException>()
                .And
                .Message.Should().BeEquivalentTo(Mensagens.Usuario_SenhaAtualInformadaInvalida);
        }
    }
}
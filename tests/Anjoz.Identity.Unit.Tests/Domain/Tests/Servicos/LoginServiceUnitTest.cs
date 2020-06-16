using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Servicos;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Entidades.Login;
using Anjoz.Identity.Domain.Servicos;
using Anjoz.Identity.Unit.Tests.Domain.Fixtures;
using Anjoz.Identity.Utils.Tests.Extensoes;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using FluentAssertions;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Domain.Tests.Servicos
{
    public class LoginServiceUnitTest : IClassFixture<LoginServiceUnitTestFixture>
    {
        private readonly ILoginService _loginService;

        public LoginServiceUnitTest(LoginServiceUnitTestFixture fixture)
        {
            _loginService = new LoginService(fixture.InitializeUserService(),
                fixture.InicializarSignInManagerService(),
                fixture.InicializarTokenProvider(),
                fixture.InicializarEntidadeVinculoService(),
                fixture.InicializarClaimService());
        }

        [Fact]
        public async Task Deve_Realizar_Login()
        {
            var login = new Login {LoginUsuario = "teste1", Senha = "123Ab@&"};

            var loginUsuario = await _loginService.Login(login);

            var resultadoEsperado = LoginUsuarioUtils.LoginUsuario;
            var claimsUsuarioId = UsuarioClaimUtils.UsuariosClaims.Select(lnq => lnq.ClaimId);
            resultadoEsperado.Claims = ClaimUtils.Claims.Where(lnq => claimsUsuarioId.Contains(lnq.Id));

            loginUsuario.Should().BeEquivalentTo(resultadoEsperado, opt =>
                opt.Excluding(prop => prop.Usuario.ConcurrencyStamp));
        }

        [Fact]
        public async Task Deve_Realizar_Login_Sem_Claims()
        {
            var login = new Login {LoginUsuario = "teste9", Senha = "123Ab@&"};

            var resultadoEsperado = new LoginUsuario
            {
                Usuario = UsuarioUtils.Usuarios.FirstOrDefault(lnq => lnq.NormalizedUserName == "TESTE9"),
                Claims = new List<Claim>(),
                AcessToken = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJ0ZXN0ZTEiLCJSRVNPVVJDRV9QRVJNSVNTSU9OUyI6WyJWQUxVRTEiLCJWQUxVRTIiLCJWQUxVRTMiLCJWQUxVRTQiLCJWQUxVRTUiXSwibmJmIjoxNTczNTE1NDc2LCJleHAiOjE1NzM1Mjc2MDAsImlhdCI6MTU3MzUxNTQ3Nn0.oGnH71MIqalOLBhXdHUfs1lYpyHFalYWaAC6dmlfvJzr0DjXZR_vV4UY91VbdXDwG5Va7_Bx4il1OiM0M0yxAw"
            };

            var loginUsuario = await _loginService.Login(login);

            loginUsuario.Claims.Should().BeNullOrEmpty();
            
            loginUsuario.Should().BeEquivalentTo(resultadoEsperado, opt =>
                opt.Excluding(prop => prop.Usuario.ConcurrencyStamp));
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Usuario_Nao_Existir()
        {
            var login = new Login {LoginUsuario = "teste99", Senha = "123Ab@&"};

            _loginService.Invoking(async lnq => await lnq.Login(login))
                .Should()
                .ValidarExcecaoPropriedade(mensagem: "Nome ou senha inválida.");
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Senha_Estiver_Incorreta()
        {
            var login = new Login {LoginUsuario = "teste1", Senha = "123aa@&"};

            _loginService.Invoking(async lnq => await lnq.Login(login))
                .Should()
                .ValidarExcecaoPropriedade(mensagem: "Nome ou senha inválida.");
        }
    }
}
using System.Linq;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Servicos.Identity;
using Anjoz.Identity.Unit.Tests.Domain.Fixtures.Identity;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using FluentAssertions;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Domain.Tests.Servicos.Identity
{
    public class SignInManagerServiceUnitTest : IClassFixture<SignInManagerServiceFixture>
    {
        private readonly ISignInManagerService _signInManagerService;

        public SignInManagerServiceUnitTest(SignInManagerServiceFixture fixture)
        {
            _signInManagerService = new SignInManagerService(fixture.InicializarSigninManager());
        }

        [Fact]
        public async Task Deve_Validar_Usuario_E_Senha()
        {
            var resultado = await _signInManagerService.ChecarSenhaUsuario(UsuarioUtils.Usuarios.FirstOrDefault(), "Abc123&&");
            resultado.Should().BeTrue();
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Quando_Usuario_Ou_Senha_Invalido()
        {
            var resultado = await _signInManagerService.ChecarSenhaUsuario(UsuarioUtils.Usuarios.FirstOrDefault(), "abc321&&&&");
            resultado.Should().BeFalse();
        }
    }
}
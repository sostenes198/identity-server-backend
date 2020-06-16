using Anjoz.Identity.Domain.Contratos.Servicos;
using Anjoz.Identity.Utils.Tests.Fixture;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using Moq;

namespace Anjoz.Identity.Unit.Tests.Application.Fixture
{
    public class LoginApplicationServiceUnitTestFixture : BaseUnitTestFixture
    {
        public ILoginService InicializarLoginService()
        {
            var mock = new Mock<ILoginService>();

            mock.Setup(lnq => 
                    lnq.Login(It.Is<Identity.Domain.Entidades.Login.Login>(t => t.LoginUsuario == "teste1" && t.Senha == "TESTE1")))
                .ReturnsAsync(LoginUsuarioUtils.LoginUsuario);

            return mock.Object;
        }
    }
}
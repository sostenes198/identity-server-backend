using System;
using System.Linq;
using System.Linq.Expressions;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Contratos.Token;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Entidades.Login;
using Anjoz.Identity.Domain.Extensoes.Paginacao;
using Anjoz.Identity.Utils.Tests.Fixture;
using Anjoz.Identity.Utils.Tests.Utils;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using Moq;

namespace Anjoz.Identity.Unit.Tests.Domain.Fixtures
{
    public class LoginServiceUnitTestFixture : BaseUnitTestFixture
    {
        public IUsuarioService InitializeUserService()
        {
            var mock = new Mock<IUsuarioService>();

            mock.Setup(lnq => lnq.ObterPorLoginAsync(It.IsAny<string>(), It.IsAny<string[]>()))
                .ReturnsAsync((string nome, string[] includes) => UsuarioUtils.Usuarios.FirstOrDefault(lnq => lnq.NormalizedUserName == nome.ToUpper()));

            return mock.Object;
        }

        public ISignInManagerService InicializarSignInManagerService()
        {
            var mock = new Mock<ISignInManagerService>();

            mock.Setup(lnq => lnq.ChecarSenhaUsuario(It.IsAny<Usuario>(), It.Is<string>(t => t == "123Ab@&"), It.IsAny<bool>()))
                .ReturnsAsync(true);

            mock.Setup(lnq => lnq.ChecarSenhaUsuario(It.IsAny<Usuario>(), It.Is<string>(t => t != "123Ab@&"), It.IsAny<bool>()))
                .ReturnsAsync(false);

            return mock.Object;
        }

        public ITokenProvider<LoginUsuario> InicializarTokenProvider()
        {
            var mock = new Mock<ITokenProvider<LoginUsuario>>();

            mock.Setup(lnq => lnq.GerarToken(It.IsAny<LoginUsuario>()))
                .ReturnsAsync(LoginUtils.Token);

            return mock.Object;
        }

        public IUsuarioClaimService InicializarEntidadeVinculoService()
        {
            var mock = new Mock<IUsuarioClaimService>();

            mock.Setup(lnq => lnq.ListarPorAsync(It.IsAny<Expression<Func<UsuarioClaim, bool>>>(),
                    It.IsAny<string[]>(), It.IsAny<IPagedParam>()))
                .ReturnsAsync((Expression<Func<UsuarioClaim, bool>> expressao, string[] includes, IPagedParam pagedParams) 
                    => UsuarioClaimUtils.UsuariosClaims.Where(expressao.Compile()).ToPagedList());

            return mock.Object;
        }

        public IClaimService InicializarClaimService()
        {
            var mock = new Mock<IClaimService>();

            mock.Setup(lnq => lnq.ListarPorAsync(It.IsAny<Expression<Func<Claim, bool>>>(),
                    It.IsAny<string[]>(), It.IsAny<IPagedParam>()))
                .ReturnsAsync((Expression<Func<Claim, bool>> expressao, string[] includes, IPagedParam pagedParams) 
                    => ClaimUtils.Claims.Where(expressao.Compile()).ToPagedList());

            return mock.Object;
        }
    }
}
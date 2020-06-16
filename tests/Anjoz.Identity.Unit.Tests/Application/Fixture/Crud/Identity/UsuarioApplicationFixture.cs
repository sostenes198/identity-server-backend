using System.Linq;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Extensoes.Paginacao;
using Anjoz.Identity.Utils.Tests.Fixture;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using Moq;

namespace Anjoz.Identity.Unit.Tests.Application.Fixture.Crud.Identity
{
    public class UsuarioApplicationFixture : BaseUnitTestFixture
    {
        public IUsuarioService InicializarUsuarioService()
        {
            var mock = new Mock<IUsuarioService>();

            mock.Setup(lnq => lnq.ObterPorNomeAsync(UsuarioUtils.NomeUsuarioTeste, It.IsAny<string[]>()))
                .ReturnsAsync(UsuarioUtils.Usuarios.First(lnq => lnq.NormalizedUserName == UsuarioUtils.NomeUsuarioTeste.ToUpper()));

            mock.Setup(lnq => lnq.ListarClaims(It.IsAny<int>(), It.IsAny<IPagedParam>()))
                .ReturnsAsync((int idUsuario, IPagedParam pagedParams) => UsuarioClaimUtils.UsuariosClaims.Where(lnq => lnq.UserId == idUsuario).Select(lnq => lnq.Claim).ToPagedList());

            return mock.Object;
        }
    }
}
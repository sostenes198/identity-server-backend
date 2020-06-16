using System.Linq;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Entidades.Paginacao;
using Anjoz.Identity.Domain.Extensoes.Paginacao;
using Anjoz.Identity.Utils.Tests.Fixture;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using Moq;

namespace Anjoz.Identity.Unit.Tests.Application.Fixture.Crud.Identity
{
    public class PerfilApplicationFixture : BaseUnitTestFixture
    {
        public IPerfilService InicializarPerfilService()
        {
            var mock = new Mock<IPerfilService>();

            mock.Setup(lnq => lnq.ObterPorNomeAsync(PerfilUtils.NomePerfilInvalido, It.IsAny<string[]>()))
                .ReturnsAsync(PerfilUtils.Perfis.First(lnq => lnq.NormalizedName == PerfilUtils.NomePerfilInvalido.ToUpper()));
            
            mock.Setup(lnq => lnq.ListarClaims(It.IsAny<int>(), It.IsAny<PagedParam>()))
                .ReturnsAsync((int roleId, IPagedParam pagedParams) => PerfilClaimUtils.PerfilClaims.Where(lnq => lnq.RoleId == roleId).Select(lnq => lnq.Claim).ToPagedList());

            return mock.Object;
        }
    }
}
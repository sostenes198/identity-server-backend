using System;
using System.Linq;
using System.Linq.Expressions;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Repositorios.Identity;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Extensoes.Paginacao;
using Anjoz.Identity.Infrastructure.Extensoes;
using Anjoz.Identity.Utils.Tests.Fixture;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using Moq;

namespace Anjoz.Identity.Unit.Tests.Domain.Fixtures.Identity
{
    public class PerfilClaimFixture : BaseUnitTestFixture
    {
        public IPerfilClaimRepository InicializarRepositorioPerfilClaim()
        {
            var mock = new Mock<IPerfilClaimRepository>();

            mock.Setup(lnq => lnq.ListarPorAsync(
                    It.IsAny<Expression<Func<PerfilClaim, bool>>>(), It.IsAny<string[]>(), It.IsAny<IPagedParam>()))
                .ReturnsAsync((Expression<Func<PerfilClaim, bool>> where, string[] includes, IPagedParam pagedParams) =>
                {
                    where ??= PredicateBuilderExtension.True<PerfilClaim>();
                    return PerfilClaimUtils.PerfilClaims.Where(where.Compile()).ToPagedList();
                });

            return mock.Object;
        }

        public IClaimService InicializarClaimService()
        {
            var mock = new Mock<IClaimService>();

            mock.Setup(lnq => lnq.ListarPorAsync(
                    It.IsAny<Expression<Func<Claim, bool>>>(), It.IsAny<string[]>(), It.IsAny<IPagedParam>()))
                .ReturnsAsync((Expression<Func<Claim, bool>> expressao, string[] includes, IPagedParam pagedParams) => ClaimUtils.Claims.Where(expressao.Compile()).ToPagedList());

            return mock.Object;
        }
    }
}
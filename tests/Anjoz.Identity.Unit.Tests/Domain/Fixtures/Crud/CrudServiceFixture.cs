using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Repositorios.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Extensoes.Paginacao;
using Anjoz.Identity.Infrastructure.Extensoes;
using Anjoz.Identity.Utils.Tests.Fixture;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using Moq;

namespace Anjoz.Identity.Unit.Tests.Domain.Fixtures.Crud
{
    public class CrudServiceFixture : BaseUnitTestFixture
    {
        public IClaimRepository InicializarCrudRepository()
        {
            var mock = new Mock<IClaimRepository>();

            mock.Setup(lnq => lnq.ObterPorIdAsync(
                    It.IsAny<int>()))
                .ReturnsAsync((int id) => ClaimUtils.Claims.FirstOrDefault(lnq => lnq.Id == id));

            mock.Setup(lnq => lnq.ListarPorAsync(
                    It.IsAny<Expression<Func<Claim, bool>>>(), It.IsAny<string[]>(), It.IsAny<IPagedParam>()))
                .ReturnsAsync((Expression<Func<Claim, bool>> where, string[] includes, IPagedParam pagedParams) =>
                {
                    where ??= PredicateBuilderExtension.True<Claim>();
                    return ClaimUtils.Claims.Where(where.Compile()).ToPagedList();
                });

            mock.Setup(lnq => lnq.CriarAsync(It.IsAny<Claim>()))
                .Returns((Claim claim) =>
                {
                    claim.Id = 1;
                    return Task.FromResult(claim);
                });

            mock.Setup(lnq => lnq.AtualizarAsync(It.IsAny<Claim>()))
                .Returns((Claim claim) => Task.FromResult(claim));

            mock.Setup(lnq => lnq.ExcluirAsync(It.IsAny<Claim>()));

            return mock.Object;
        }
    }
}
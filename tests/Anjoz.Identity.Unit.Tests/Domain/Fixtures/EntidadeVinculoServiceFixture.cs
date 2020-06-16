using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Repositorios.Identity;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Extensoes.Paginacao;
using Anjoz.Identity.Utils.Tests.Fixture;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using Moq;

namespace Anjoz.Identity.Unit.Tests.Domain.Fixtures
{
    public class EntidadeVinculoServiceFixture : BaseUnitTestFixture
    {
        public IUsuarioClaimRepository InitializeUserClaimRepository()
        {
            var mock = new Mock<IUsuarioClaimRepository>();

            mock.Setup(lnq => lnq.ListarPorAsync(It.IsAny<Expression<Func<UsuarioClaim, bool>>>(),
                    It.IsAny<string[]>(), It.IsAny<IPagedParam>()))
                .ReturnsAsync((Expression<Func<UsuarioClaim, bool>> expressao, string[] includes, IPagedParam pagedParams) => UsuariosClaims.Where(expressao.Compile()).ToPagedList());

            return mock.Object;
        }

        public IClaimService InitializeClaimService()
        {
            var mock = new Mock<IClaimService>();

            mock.Setup(lnq => lnq.ListarPorAsync(It.IsAny<Expression<Func<Claim, bool>>>(),
                    It.IsAny<string[]>(), It.IsAny<IPagedParam>()))
                .ReturnsAsync((Expression<Func<Claim, bool>> where, string[] includes, IPagedParam pagedParams) => ClaimUtils.Claims.Where(where.Compile()).ToPagedList());

            return mock.Object;
        }
        
        private ICollection<UsuarioClaim> UsuariosClaims => new List<UsuarioClaim>
        {
            new UsuarioClaim {UserId = 1, ClaimId = 1},
            new UsuarioClaim {UserId = 1, ClaimId = 2},
            new UsuarioClaim {UserId = 1, ClaimId = 3},
            new UsuarioClaim {UserId = 1, ClaimId = 4},
            new UsuarioClaim {UserId = 1, ClaimId = 5}
        };
    }
}
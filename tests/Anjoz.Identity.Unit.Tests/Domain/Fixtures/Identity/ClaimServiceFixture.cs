using Anjoz.Identity.Domain.Contratos.Repositorios.Identity;
using Anjoz.Identity.Utils.Tests.Fixture;
using Moq;

namespace Anjoz.Identity.Unit.Tests.Domain.Fixtures.Identity
{
    public class ClaimServiceFixture : BaseUnitTestFixture
    {
        public IClaimRepository InicializarClaimRepository()
        {
            var mock = new Mock<IClaimRepository>();

            return mock.Object;
        }
    }
}
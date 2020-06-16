using Anjoz.Identity.Infrastructure;
using Anjoz.Identity.Utils.Tests.Fixture;

namespace Anjoz.Identity.Unit.Tests.Application.Fixture.Token
{
    public class TokenProviderUnitTestFixture : BaseUnitTestFixture
    {
        public TokenProviderUnitTestFixture()
        {
            AddServices = (services) =>
                Bootstrap.ConfigurarOptions(services, Configuration);
        }
    }
}
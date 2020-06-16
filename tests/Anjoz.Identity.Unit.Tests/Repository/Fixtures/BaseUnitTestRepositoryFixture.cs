using Anjoz.Identity.Repository;
using Anjoz.Identity.Utils.Tests.Fixture;
using Microsoft.Extensions.DependencyInjection;

namespace Anjoz.Identity.Unit.Tests.Repository.Fixtures
{
    public class BaseUnitTestRepositoryFixture : BaseUnitTestFixture
    {
        public BaseUnitTestRepositoryFixture()
        {
            AddServices = (services) =>
            {
                services.RegistrarRepositoryServices(Configuration);
                services.AddSingleton(provider => Configuration);
                services.AddAuthentication();
            };
        }
    }
}
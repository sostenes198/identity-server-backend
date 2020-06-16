using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Utils.Tests.Fixture;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Anjoz.Identity.Unit.Tests.Domain.Fixtures.Identity
{
    public class SignInManagerServiceFixture : BaseUnitTestFixture
    {
        public SignInManager<Usuario> InicializarSigninManager()
        {
            var mock = IdentityMock.MockSignInManager<Usuario>();

            mock.Setup(lnq => lnq.CheckPasswordSignInAsync(It.IsAny<Usuario>(),
                    It.Is<string>(t => t == "Abc123&&"), It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success);
            
            mock.Setup(lnq => lnq.CheckPasswordSignInAsync(It.IsAny<Usuario>(),
                    It.Is<string>(t => t != "Abc123&&"), It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Failed);

            return mock.Object;
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Anjoz.Identity.Utils.Tests.Fixture
{
    public class IdentityMock
    {
        public static Mock<SignInManager<TUser>> MockSignInManager<TUser>() where TUser : class
        {
            return new Mock<SignInManager<TUser>>(
                MockUserManager<TUser>().Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<TUser>>(),
                null,
                null,
                null);
        }

        public static Mock<UserManager<TUser>> MockUserManager<TUser>(
            IPasswordHasher<TUser> passwordHasher = null) where TUser : class
        {
            var userManagerMock = new Mock<UserManager<TUser>>(
                Mock.Of<IUserStore<TUser>>(), null, passwordHasher, null, null, null,
                null, null, null);
            userManagerMock.Object.UserValidators.Add(new UserValidator<TUser>());
            userManagerMock.Object.PasswordValidators.Add(new PasswordValidator<TUser>());
            
            return userManagerMock;
        }
    }
}
using System;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Utils.Tests.Fakers.Base;

namespace Anjoz.Identity.Utils.Tests.Fakers
{
    public sealed class LoginFaker : FakerBase<UsuarioLogin>
    {
        public LoginFaker()
        {
            RuleFor(lnq => lnq.LoginProvider, (f, u) => Guid.NewGuid().ToString());
            RuleFor(lnq => lnq.ProviderKey, (f, u) => Guid.NewGuid().ToString());
            RuleFor(lnq => lnq.ProviderDisplayName, (f, u) => f.Lorem.Word());
        }
    }
}
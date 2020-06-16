using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Utils.Tests.Fakers.Base;

namespace Anjoz.Identity.Utils.Tests.Fakers.Identity
{
    public sealed class UsuarioFaker : FakerBase<Usuario>
    {
        public UsuarioFaker()
        {
            RuleFor(lnq => lnq.UserName, (f, u) => f.Internet.UserName());
            RuleFor(lnq => lnq.Login, (f, u) => f.Internet.UserName());
            RuleFor(lnq => lnq.Email, (f, u) => f.Internet.Email(u.UserName));
            RuleFor(lnq => lnq.PhoneNumber, (f, u) => f.Phone.PhoneNumber());
            RuleFor(lnq => lnq.CodigoEquipe, (f, u) => f.Random.Int(0, 999));
        }
    }
}
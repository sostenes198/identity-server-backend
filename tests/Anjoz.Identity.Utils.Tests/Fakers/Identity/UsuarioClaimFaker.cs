using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Utils.Tests.Fakers.Base;

namespace Anjoz.Identity.Utils.Tests.Fakers.Identity
{
    public sealed class UsuarioClaimFaker : FakerBase<UsuarioClaim>
    {
        public UsuarioClaimFaker(int idUser = 1)
        {
            RuleFor(lnq => lnq.UserId, (f, u) => idUser);
            RuleFor(lnq => lnq.ClaimId, (f, u) => f.UniqueIndex);
        }
    }
}
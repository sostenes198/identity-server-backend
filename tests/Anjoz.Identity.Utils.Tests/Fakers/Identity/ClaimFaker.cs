using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Utils.Tests.Fakers.Base;

namespace Anjoz.Identity.Utils.Tests.Fakers.Identity
{
    public sealed class ClaimFaker : FakerBase<Claim>
    {
        public ClaimFaker()
        {
            RuleFor(lnq => lnq.Valor, f => f.Lorem.Word());
            RuleFor(lnq => lnq.Descricao, f => f.Lorem.Word());
        }
    }
}
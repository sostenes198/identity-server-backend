using System;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Utils.Tests.Fakers.Base;

namespace Anjoz.Identity.Utils.Tests.Fakers.Identity
{
    public sealed class PerfilFaker : FakerBase<Perfil>
    {
        public PerfilFaker()
        {
            RuleFor(lnq => lnq.Name, (f, u) => Guid.NewGuid().ToString());
        }
    }
}
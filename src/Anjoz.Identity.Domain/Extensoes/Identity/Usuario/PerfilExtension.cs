using System.Collections.Generic;
using Anjoz.Identity.Domain.Entidades.Identity;

namespace Anjoz.Identity.Domain.Extensoes.Identity.Usuario
{
    public static class PerfilExtension
    {
        public static IEnumerable<PerfilClaim> ObterPerfilClaimELimpar(Perfil perfil)
        {
            var perfilClaim = perfil.PerfisClaims;
            perfil.PerfisClaims = default;
            return perfilClaim;
        }
    }
}
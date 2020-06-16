using Microsoft.AspNetCore.Identity;

namespace Anjoz.Identity.Domain.Entidades.Identity
{
    public class PerfilClaim : IdentityRoleClaim<int>
    {
        public Perfil Perfil { get; set; }

        public int ClaimId { get; set; }
        
        public Claim Claim { get; set; }
    }
}
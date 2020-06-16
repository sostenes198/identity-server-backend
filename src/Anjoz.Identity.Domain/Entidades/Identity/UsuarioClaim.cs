using Microsoft.AspNetCore.Identity;

namespace Anjoz.Identity.Domain.Entidades.Identity
{
    public class UsuarioClaim : IdentityUserClaim<int>
    {
        public Usuario Usuario { get; set; }

        public int ClaimId { get; set; }
        
        public Claim Claim { get; set; }
    }
}
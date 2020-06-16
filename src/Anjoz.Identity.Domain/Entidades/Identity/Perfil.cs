using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Anjoz.Identity.Domain.Entidades.Identity
{
    public class Perfil : IdentityRole<int>
    {
        public Perfil()
        {
            PerfisClaims = new HashSet<PerfilClaim>();
            UsuarioPerfis = new HashSet<UsuarioPerfil>();
        }
        
        public IEnumerable<PerfilClaim> PerfisClaims { get; set; }
        
        public IEnumerable<UsuarioPerfil> UsuarioPerfis { get; set; }
    }
}
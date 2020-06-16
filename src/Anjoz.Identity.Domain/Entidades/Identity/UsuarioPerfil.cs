using Microsoft.AspNetCore.Identity;

namespace Anjoz.Identity.Domain.Entidades.Identity
{
    public class UsuarioPerfil : IdentityUserRole<int>
    {
        public Usuario Usuario { get; set; }
        public Perfil Perfil { get; set; }
    }
}
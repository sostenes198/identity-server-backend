using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Anjoz.Identity.Domain.Entidades.Identity
{
    public class Usuario : IdentityUser<int>
    {
        public Usuario()
        {
            UsuariosClaims = new HashSet<UsuarioClaim>();
            UsuariosPerfis = new HashSet<UsuarioPerfil>();
        }

        public string Login { get; set; }

        public string LoginNormalizado
        {
            get => Login?.ToUpper();
            set { }
        }

        public int? CodigoEquipe { get; set; }

        public IEnumerable<UsuarioClaim> UsuariosClaims { get; set; }

        public IEnumerable<UsuarioPerfil> UsuariosPerfis { get; set; }
    }
}
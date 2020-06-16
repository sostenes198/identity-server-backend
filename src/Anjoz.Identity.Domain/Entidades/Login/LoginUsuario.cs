using System.Collections.Generic;
using Anjoz.Identity.Domain.Entidades.Identity;

namespace Anjoz.Identity.Domain.Entidades.Login
{
    public class LoginUsuario
    {
        public LoginUsuario()
        {
            Usuario = new Usuario();
            Claims = new HashSet<Claim>();
        }

        public Usuario Usuario { get; set; }
        public string AcessToken { get; set; }
        public IEnumerable<Claim> Claims { get; set; }

        public static LoginUsuario Inicializar(Usuario usuario, IEnumerable<Claim> claims) => new LoginUsuario {Usuario = usuario, Claims = claims};
    }
}
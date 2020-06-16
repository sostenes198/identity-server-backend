using System.Collections.Generic;
using Anjoz.Identity.Domain.Entidades.Identity;

namespace Anjoz.Identity.Utils.Tests.Utils.Identity
{
   public sealed class UsuarioPerfilUtils
    {
        public static readonly ICollection<UsuarioPerfil> UsuariosPerfis = new List<UsuarioPerfil>
        {
            new UsuarioPerfil {UserId = 1, RoleId = 1},
            new UsuarioPerfil {UserId = 1, RoleId = 2},
            new UsuarioPerfil {UserId = 1, RoleId = 3},
            new UsuarioPerfil {UserId = 1, RoleId = 4},
            new UsuarioPerfil {UserId = 1, RoleId = 5}
        };
    }
}
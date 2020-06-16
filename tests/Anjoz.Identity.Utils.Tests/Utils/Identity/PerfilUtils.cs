using System.Collections.Generic;
using Anjoz.Identity.Domain.Entidades.Identity;

namespace Anjoz.Identity.Utils.Tests.Utils.Identity
{
    public sealed class PerfilUtils
    {
        public const string NomePerfilInvalido = "PerfilInvalido";

        public static readonly ICollection<Perfil> Perfis = new List<Perfil>
        {
            new Perfil {Id = 1, Name = "PerfilTeste1", NormalizedName = "PerfilTeste1".ToUpper()},
            new Perfil {Id = 2, Name = "PerfilTeste2", NormalizedName = "PerfilTeste2".ToUpper()},
            new Perfil {Id = 3, Name = "PerfilTeste3", NormalizedName = "PerfilTeste3".ToUpper()},
            new Perfil {Id = 4, Name = "CCPerfilTeste4", NormalizedName = "CCPerfilTeste4".ToUpper()},
            new Perfil {Id = 5, Name = "CCPerfilTeste5", NormalizedName = "CCPerfilTeste5".ToUpper()},
            new Perfil {Id = 9998, Name = NomePerfilInvalido, NormalizedName = NomePerfilInvalido.ToUpper()}
        };
    }
}
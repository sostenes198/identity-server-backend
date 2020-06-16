using System.Collections.Generic;
using Anjoz.Identity.Domain.Entidades.Identity;

namespace Anjoz.Identity.Utils.Tests.Utils.Identity
{
    public sealed class UsuarioClaimUtils
    {
        public static ICollection<UsuarioClaim> UsuariosClaims => new List<UsuarioClaim>
        {
            new UsuarioClaim {ClaimId = 1, UserId = 1, Claim = new Claim {Id = 1, Valor = "Value1", ValorNormalizado = "VALUE1", Descricao = "Descricao1", DescricaNormalizada = "DESCRICAO1"},},
            new UsuarioClaim {ClaimId = 2, UserId = 1, Claim = new Claim {Id = 2, Valor = "Value2", ValorNormalizado = "VALUE2", Descricao = "Descricao2", DescricaNormalizada = "DESCRICAO2"},},
            new UsuarioClaim {ClaimId = 3, UserId = 1, Claim = new Claim {Id = 3, Valor = "Value3", ValorNormalizado = "VALUE3", Descricao = "Descricao3", DescricaNormalizada = "DESCRICAO3"},},
            new UsuarioClaim {ClaimId = 4, UserId = 1, Claim = new Claim {Id = 4, Valor = "Value4", ValorNormalizado = "VALUE4", Descricao = "Descricao4", DescricaNormalizada = "DESCRICAO4"},},
            new UsuarioClaim {ClaimId = 5, UserId = 1, Claim = new Claim {Id = 5, Valor = "Value5", ValorNormalizado = "VALUE5", Descricao = "Descricao5", DescricaNormalizada = "DESCRICAO5"}}
        };
    }
}
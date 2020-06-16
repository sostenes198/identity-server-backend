using System.Collections.Generic;
using Anjoz.Identity.Domain.Entidades.Identity;

namespace Anjoz.Identity.Utils.Tests.Utils.Identity
{
    public sealed class PerfilClaimUtils
    {
        public static readonly ICollection<PerfilClaim> PerfilClaims = new List<PerfilClaim>
        {
            new PerfilClaim {ClaimId = 1, RoleId = 1, Claim = new Claim {Id = 1, Valor = "Value1", ValorNormalizado = "VALUE1", Descricao = "Descricao1", DescricaNormalizada = "DESCRICAO1"},},
            new PerfilClaim {ClaimId = 2, RoleId = 1, Claim = new Claim {Id = 2, Valor = "Value2", ValorNormalizado = "VALUE2", Descricao = "Descricao2", DescricaNormalizada = "DESCRICAO2"},},
            new PerfilClaim {ClaimId = 3, RoleId = 1, Claim = new Claim {Id = 3, Valor = "Value3", ValorNormalizado = "VALUE3", Descricao = "Descricao3", DescricaNormalizada = "DESCRICAO3"},},
            new PerfilClaim {ClaimId = 4, RoleId = 1, Claim = new Claim {Id = 4, Valor = "Value4", ValorNormalizado = "VALUE4", Descricao = "Descricao4", DescricaNormalizada = "DESCRICAO4"},},
            new PerfilClaim {ClaimId = 5, RoleId = 1, Claim = new Claim {Id = 5, Valor = "Value5", ValorNormalizado = "VALUE5", Descricao = "Descricao5", DescricaNormalizada = "DESCRICAO5"}}
        };
    }
}
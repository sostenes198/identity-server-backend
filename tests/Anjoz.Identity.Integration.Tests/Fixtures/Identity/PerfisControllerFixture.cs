using System.Collections.Generic;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Integration.Tests.Fixtures.Base;

namespace Anjoz.Identity.Integration.Tests.Fixtures.Identity
{
    public class PerfisControllerFixture : BaseIntegrationTestFixture
    {
        public readonly ICollection<Claim> Claims = new List<Claim>
        {
            new Claim {Valor = "PerfilBBValor1", Descricao = "PerfilBBValor1"},
            new Claim {Valor = "PerfilBBValor2", Descricao = "PerfilBBValor2"},
            new Claim {Valor = "PerfilBBValor3", Descricao = "PerfilBBValor3"},
            new Claim {Valor = "PerfilBBValor4", Descricao = "PerfilBBValor4"},
        };
        
        public readonly ICollection<Claim> PerfilClaims = new List<Claim>
        {
            new Claim {Valor = "PerfilClaimBBValor1", Descricao = "PerfilClaimBBValor1"},
            new Claim {Valor = "PerfilClaimBBValor2", Descricao = "PerfilClaimBBValor2"},
            new Claim {Valor = "PerfilClaimBBValor3", Descricao = "PerfilClaimBBValor3"},
            new Claim {Valor = "PerfilClaimBBValor4", Descricao = "PerfilClaimBBValor4"},
        };
        
        public readonly ICollection<Claim> ClaimsAtualizar = new List<Claim>
        {
            new Claim {Valor = "PerfilAABBValor1", Descricao = "PerfilAABBValor1"},
            new Claim {Valor = "PerfilAABBValor2", Descricao = "PerfilAABBValor2"},
        };
    }
}
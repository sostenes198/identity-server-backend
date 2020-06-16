using Anjoz.Identity.Domain.Entidades.Identity;

namespace Anjoz.Identity.Domain.Contratos.Servicos.Identity
{
    public interface IUsuarioClaimService : IEntidadeVinculoService<UsuarioClaim, int, Claim, int>
    {
        
    }
}
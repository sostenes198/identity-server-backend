using Anjoz.Identity.Application.Contratos.Crud;
using Anjoz.Identity.Application.Dtos.Identity.Claim;
using Anjoz.Identity.Domain.Entidades.Identity;

namespace Anjoz.Identity.Application.Contratos.Identity
{
    public interface IClaimApplicationService : ICrudApplicationService<ClaimDto, int, Claim, ClaimFiltroDto,
        ClaimCriarDto, ClaimAtualizarDto>
    {
    }
}
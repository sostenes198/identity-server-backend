using Anjoz.Identity.Application.Contratos.Identity;
using Anjoz.Identity.Application.Dtos.Identity.Claim;
using Anjoz.Identity.Application.Servicos.Crud;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Infrastructure.Interfaces.Filter;
using AutoMapper;

namespace Anjoz.Identity.Application.Servicos.Identity
{
    public class ClaimApplicationService : CrudApplicationService<ClaimDto, int, Claim, ClaimFiltroDto, ClaimCriarDto, ClaimAtualizarDto>, IClaimApplicationService
    {
        public ClaimApplicationService(IClaimService claimService, IMapper mapper, IGeradorFiltro<Claim, ClaimFiltroDto> geradorFiltro)
            : base(claimService, mapper, geradorFiltro)
        {
        }
    }
}
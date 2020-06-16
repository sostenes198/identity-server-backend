using Anjoz.Identity.Application.Contratos.Identity;
using Anjoz.Identity.Application.Dtos.Identity.Claim;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.WebApi.Controllers.Base;

namespace Anjoz.Identity.WebApi.Controllers.Identity
{
    public class ClaimsController : CrudControllerBase<ClaimDto, int, Claim, ClaimFiltroDto, ClaimCriarDto, ClaimAtualizarDto>
    {
        public ClaimsController(IClaimApplicationService applicationService) : base(applicationService)
        {
        }
    }
}
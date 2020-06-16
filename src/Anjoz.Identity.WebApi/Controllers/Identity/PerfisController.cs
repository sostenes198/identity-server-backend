using System.Threading.Tasks;
using Anjoz.Identity.Application.Contratos.Identity;
using Anjoz.Identity.Application.Dtos.Base;
using Anjoz.Identity.Application.Dtos.Identity.Claim;
using Anjoz.Identity.Application.Dtos.Identity.Perfil;
using Anjoz.Identity.Application.Dtos.Paginacao;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Anjoz.Identity.WebApi.Controllers.Identity
{
    public class PerfisController : CrudControllerBase<PerfilDto, int, Perfil, PerfilFiltroDto, PerfilCriarDto, PerfilAtualizarDto>
    {
        private readonly IPerfilApplicationService _applicationService;

        public PerfisController(IPerfilApplicationService applicationService) : base(applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet("nome/{nome}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PerfilDto>> ObterPorNomeAsync(string nome)
        {
            var resultado = await _applicationService.ObterPorNomeAsync(nome);

            if (resultado == default)
                return NotFound();

            return Ok(resultado);
        }
        
        [HttpGet("{idPerfil}/claims")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedListDto<ClaimDto>>> ListarClaims(int idPerfil, [FromQuery] PagedParamFiltroDto pagedParam)
        {
            var resultado = await _applicationService.ListarClaims(idPerfil, pagedParam);

            if (resultado == default)
                return NotFound();

            return Ok(resultado);
        }
    }
}
using System.Threading.Tasks;
using Anjoz.Identity.Application.Contratos.Identity;
using Anjoz.Identity.Application.Dtos.Base;
using Anjoz.Identity.Application.Dtos.Identity.Claim;
using Anjoz.Identity.Application.Dtos.Identity.Usuario;
using Anjoz.Identity.Application.Dtos.Identity.Usuario.AlteracaoSenha;
using Anjoz.Identity.Application.Dtos.Paginacao;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Anjoz.Identity.WebApi.Controllers.Identity
{
    public class UsuariosController : CrudControllerBase<UsuarioDto, int, Usuario, UsuarioFiltroDto, UsuarioCriarDto, UsuarioAtualizarDto>
    {
        private readonly IUsuarioApplicationService _applicationService;

        public UsuariosController(IUsuarioApplicationService applicationService) : base(applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet("nome/{nome}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UsuarioDto>> ObterPorNomeAsync(string nome)
        {
            var resultado = await _applicationService.ObterPorNomeAsync(nome);

            if (resultado == default)
                return NotFound();

            return Ok(resultado);
        }

        [HttpGet("{idUsuario}/claims")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedListDto<ClaimDto>>> ListarClaims(int idUsuario, [FromQuery] PagedParamFiltroDto pagedParam)
        {
            var resultado = await _applicationService.ListarClaims(idUsuario, pagedParam);

            if (resultado == default)
                return NotFound();

            return Ok(resultado);
        }

        [HttpPut("senha")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UsuarioAlteracaoSenhaResultadoDto>> AlterarSenha(UsuarioAlteracaoSenhaDto usuarioAlteracaoSenha)
        {
            var resultado = await _applicationService.AlterarSenha(usuarioAlteracaoSenha);
            return Ok(resultado); 
        }
    }
}
using System.Threading.Tasks;
using Anjoz.Identity.Application.Contratos.Crud;
using Anjoz.Identity.Application.Dtos.Base;
using Anjoz.Identity.Application.Dtos.Paginacao;
using Microsoft.AspNetCore.Mvc;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace Anjoz.Identity.WebApi.Controllers.Base
{
    public class CrudControllerBase<TDto, TId, TEntidade, TFiltroDto, TCriarDto, TAtualizarDto> : ControllerBase
        where TDto : EntidadeDto
        where TEntidade : class
        where TFiltroDto : FiltroDto
        where TCriarDto : CriarEntidadeDto
        where TAtualizarDto : AtualizarEntidadeDto
    {
        private readonly ICrudApplicationService<TDto, TId, TEntidade, TFiltroDto, TCriarDto, TAtualizarDto> _applicationService;

        public CrudControllerBase(ICrudApplicationService<TDto, TId, TEntidade, TFiltroDto, TCriarDto, TAtualizarDto> applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<ActionResult<PagedListDto<TDto>>> ListarTodosAsync([FromQuery] PageListFiltroDto<TFiltroDto> filtro) => AoListarTodosAsync(filtro);

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<ActionResult<TDto>> ObterAsync(TId id) => AoObterAsync(id);


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<ActionResult<TDto>> CriarAsync(TCriarDto criarDto) => AoCriarAsync(criarDto);

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<ActionResult<TDto>> AtualizarAsync(TAtualizarDto atualizarDto) => AoAtualizarAsync(atualizarDto);


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<ActionResult<TId>> DeletarAsync(TId id) => AoDeletarAsync(id);

        protected virtual async Task<ActionResult<PagedListDto<TDto>>> AoListarTodosAsync(PageListFiltroDto<TFiltroDto> filtro)
        {
            var resultado = await _applicationService.ListarPorAsync(filtro.Filtro, filtro.PagedParam);

            return Ok(resultado);
        }

        protected virtual async Task<ActionResult<TDto>> AoObterAsync(TId id)
        {
            var resultado = await _applicationService.ObterPorIdAsync(id);

            if (resultado == default)
                return NotFound();

            return Ok(resultado);
        }

        protected virtual async Task<ActionResult<TDto>> AoCriarAsync(TCriarDto entityDto)
        {
            var resultado = await _applicationService.CriarAsync(entityDto);
            return Created(nameof(ObterAsync), resultado);
        }

        protected virtual async Task<ActionResult<TDto>> AoAtualizarAsync(TAtualizarDto entityDto)
        {
            var resultado = await _applicationService.AtualizarAsync(entityDto);
            return Ok(resultado);
        }

        protected virtual async Task<ActionResult<TId>> AoDeletarAsync(TId id)
        {
            var resultado = await _applicationService.ExcluirAsync(id);
            return Ok(resultado);
        }
    }
}
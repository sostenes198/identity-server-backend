using System.Threading.Tasks;
using Anjoz.Identity.Application.Contratos.Crud;
using Anjoz.Identity.Application.Dtos.Base;
using Anjoz.Identity.Application.Dtos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Servicos.Crud;
using Anjoz.Identity.Infrastructure.Interfaces.Filter;
using AutoMapper;

namespace Anjoz.Identity.Application.Servicos.Crud
{
    public class CrudApplicationService<TDto, TId, TEntidade, TFiltroDto, TCriarDto, TAtualizarDto> :
        ICrudApplicationService<TDto, TId, TEntidade, TFiltroDto, TCriarDto, TAtualizarDto>
        where TDto : EntidadeDto
        where TEntidade : class
        where TFiltroDto : FiltroDto
        where TCriarDto : CriarEntidadeDto
        where TAtualizarDto : AtualizarEntidadeDto
    {
        private readonly ICrudService<TEntidade, TId> _entidadeService;
        private readonly IMapper _mapper;
        private readonly IGeradorFiltro<TEntidade, TFiltroDto> _geradorFiltro;

        public CrudApplicationService(ICrudService<TEntidade, TId> entidadeService, IMapper mapper, IGeradorFiltro<TEntidade, TFiltroDto> geradorFiltro)
        {
            _entidadeService = entidadeService;
            _mapper = mapper;
            _geradorFiltro = geradorFiltro;
        }

        public Task<TDto> ObterPorIdAsync(TId id) => AoObterPorIdAsync(id);

        public Task<PagedListDto<TDto>> ListarPorAsync(TFiltroDto filtro = default, PagedParamFiltroDto pagedParam = default) => AoListarPorAsync(filtro, pagedParam);

        public Task<TDto> CriarAsync(TCriarDto criarDto) => AoCriarAsync(criarDto);

        public Task<TDto> AtualizarAsync(TAtualizarDto atualizarDto) => AoAtualizarAsync(atualizarDto);

        public Task<TId> ExcluirAsync(TId id) => AoExcluirAsync(id);

        protected virtual Task<TDto> AoObterPorIdAsync(TId id)
        {
            return _entidadeService.ObterPorIdAsync(id)
                .ContinueWith(tsk => _mapper.Map<TEntidade, TDto>(tsk.GetAwaiter().GetResult()));
        }

        protected virtual Task<PagedListDto<TDto>> AoListarPorAsync(TFiltroDto filter = default, PagedParamFiltroDto pagedParam = default)
        {
            return _entidadeService.ListarPorAsync(_geradorFiltro.Gerar(filter), default, MappePageParam(pagedParam))
                .ContinueWith(tsk => _mapper.Map<IPagedList<TEntidade>, PagedListDto<TDto>>(tsk.GetAwaiter().GetResult()));
        }

        protected virtual Task<TDto> AoCriarAsync(TCriarDto entidadeDto)
        {
            var entidade = _mapper.Map<TCriarDto, TEntidade>(entidadeDto);
            return _entidadeService.CriarAsync(entidade)
                .ContinueWith(tsk => _mapper.Map<TEntidade, TDto>(entidade));
        }

        protected virtual Task<TDto> AoAtualizarAsync(TAtualizarDto entidadeAtualizarDto)
        {
            var entidade = _mapper.Map<TAtualizarDto, TEntidade>(entidadeAtualizarDto);
            return _entidadeService.AtualizarAsync(entidade)
                .ContinueWith(tsk => _mapper.Map<TEntidade, TDto>(entidade));
        }

        protected virtual Task<TId> AoExcluirAsync(TId id)
        {
            return _entidadeService.ExcluirAsync(id)
                .ContinueWith(tsk => id);
        }

        protected IPagedParam MappePageParam(PagedParamFiltroDto pagedParam) => _mapper.Map<PagedParamFiltroDto, IPagedParam>(pagedParam);
    }
}
using System.Threading.Tasks;
using Anjoz.Identity.Application.Dtos.Base;
using Anjoz.Identity.Application.Dtos.Paginacao;

namespace Anjoz.Identity.Application.Contratos.Crud
{
    public interface IReadApplicationService<TDto, TEntidade, in TId, in TFiltroDto>
        where TDto : EntidadeDto
        where TEntidade : class
        where TFiltroDto : FiltroDto
    {
        Task<TDto> ObterPorIdAsync(TId id);

        Task<PagedListDto<TDto>> ListarPorAsync(TFiltroDto filtro = default, PagedParamFiltroDto pagedParam = default);
    }
}
using Anjoz.Identity.Application.Dtos.Base;

namespace Anjoz.Identity.Application.Dtos.Paginacao
{
    public class PageListFiltroDto<TFiltroDto> : FiltroDto
        where TFiltroDto : FiltroDto
    {
        public PagedParamFiltroDto PagedParam { get; set; }

        public TFiltroDto Filtro { get; set; }
    }
}
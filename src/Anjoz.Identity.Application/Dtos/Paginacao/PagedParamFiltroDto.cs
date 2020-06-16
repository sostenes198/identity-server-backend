using Anjoz.Identity.Domain.Entidades.Paginacao;

namespace Anjoz.Identity.Application.Dtos.Paginacao
{
    public class PagedParamFiltroDto
    {
        public int PageNumber { get; set; } = PagedParam.NumeroPaginaPadrao;

        public int PageSize { get; set; } = PagedParam.TamanhoPaginaPadrao;
    }
}
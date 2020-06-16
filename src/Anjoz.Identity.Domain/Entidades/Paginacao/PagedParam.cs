using Anjoz.Identity.Domain.Contratos.Paginacao;

namespace Anjoz.Identity.Domain.Entidades.Paginacao
{
    public class PagedParam : IPagedParam
    {
        public const int QuantidadePaginasPadrao = 1;
        public const int TamanhoPaginaPadrao = 100;
        public const int NumeroPaginaPadrao = 1;
        
        public int TotalPages { get; set; } = QuantidadePaginasPadrao;

        public int PageSize { get; set; } = TamanhoPaginaPadrao;
        public int PageNumber { get; set; } = NumeroPaginaPadrao;

        public PagedParam()
        {
        }

        public PagedParam(int pageSize, int pageNumber)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        public PagedParam Next()
        {
            PageNumber += 1;
            return this;
        }
    }
}
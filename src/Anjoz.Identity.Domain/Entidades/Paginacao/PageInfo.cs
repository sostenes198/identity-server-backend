using Anjoz.Identity.Domain.Contratos.Paginacao;

namespace Anjoz.Identity.Domain.Entidades.Paginacao
{
    public class PageInfo : IPageInfo
    {
        public PageInfo()
        {
        }

        public PageInfo(IPagedParam pagedParam)
        {
            PageNumber = pagedParam.PageNumber;
            PageSize = pagedParam.PageSize;
            TotalPages = pagedParam.TotalPages;
        }

        public int TotalPages { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public bool HasPrevious => PageNumber > 1;

        public bool HasNext => PageNumber < TotalPages;
    }
}
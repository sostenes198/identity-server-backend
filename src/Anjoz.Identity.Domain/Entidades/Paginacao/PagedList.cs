using System.Collections.Generic;
using Anjoz.Identity.Domain.Contratos.Paginacao;

namespace Anjoz.Identity.Domain.Entidades.Paginacao
{
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public IPageInfo PageInfo { get; set; }

        public PagedList(IEnumerable<T> items, IPagedParam pagedParam)
            : this(items, pagedParam.PageNumber, pagedParam.PageSize, pagedParam.TotalPages)
        {
        }

        public PagedList(IEnumerable<T> items, int pageNumber, int pageSize, int totalPages)
        {
            PageInfo = new PageInfo
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
            AddRange(items);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Entidades.Paginacao;

namespace Anjoz.Identity.Domain.Extensoes.Paginacao
{
    public static class PagedListExtensions
    {
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> items)
        {
            var result = items.ToList();
            return new PagedList<T>(result, 1, result.Count, 1);
        }
        
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> items, IPagedParam pagedParam)
        {
            var result = items.ToList();
            return new PagedList<T>(result, pagedParam ?? new PagedParam());
        }
    }
}
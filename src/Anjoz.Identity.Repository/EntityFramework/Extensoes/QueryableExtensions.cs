using System;
using System.Linq;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Microsoft.EntityFrameworkCore;

namespace Anjoz.Identity.Repository.EntityFramework.Extensoes
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> PopularIncludes<T>(this IQueryable<T> queryable, string[] includes)
            where T : class
        {
            foreach (var include in includes)
                queryable = queryable.Include(include);

            return queryable;
        }

        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, IPagedParam pagedParam)
        {
            var count = queryable.CountAsync().GetAwaiter().GetResult();
            pagedParam.TotalPages = (int) Math.Ceiling(count / (double) pagedParam.PageSize);

            queryable =  queryable.Skip((pagedParam.PageNumber - 1) * pagedParam.PageSize).Take(pagedParam.PageSize);

            return queryable;
        }
    }
}
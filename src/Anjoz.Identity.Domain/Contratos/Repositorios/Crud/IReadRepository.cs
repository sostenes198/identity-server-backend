using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Paginacao;

namespace Anjoz.Identity.Domain.Contratos.Repositorios.Crud
{
    public interface IReadRepository<T, in TId>
        where T : class
    {
        Task<T> ObterPorIdAsync(TId id);
        Task<IPagedList<T>> ListarPorAsync(Expression<Func<T, bool>> where = default, string[] includes = default, IPagedParam pagedParam = default);
    }
}
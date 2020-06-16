using System.Collections.Generic;

namespace Anjoz.Identity.Domain.Contratos.Paginacao
{
    public interface IPagedList<T> : IList<T>, IPagedInfo
    {
    }
}
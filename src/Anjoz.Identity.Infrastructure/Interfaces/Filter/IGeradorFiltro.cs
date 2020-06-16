using System;
using System.Linq.Expressions;

namespace Anjoz.Identity.Infrastructure.Interfaces.Filter
{
    public interface IGeradorFiltro<T, in TFiltro>
        where T : class
    {
        Expression<Func<T, bool>> Gerar(TFiltro filtro);
    }
}
using System;
using System.Linq.Expressions;
using Anjoz.Identity.Infrastructure.Extensoes;
using Anjoz.Identity.Infrastructure.Interfaces.Filter;

namespace Anjoz.Identity.Application.Filtros.Base
{
    public  abstract class GeradorFiltro<T, TFiltro> : IGeradorFiltro<T, TFiltro>
        where T : class 
    {
        public Expression<Func<T, bool>> Gerar(TFiltro filtro)
        {
            var expressao = PredicateBuilderExtension.True<T>();

            return filtro != null ? Gerar(filtro, expressao) : expressao;
        }

        protected abstract Expression<Func<T, bool>> Gerar(TFiltro filtro, Expression<Func<T, bool>> expressao);
    }
}
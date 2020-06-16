using System;
using System.Linq.Expressions;
using Anjoz.Identity.Application.Dtos.Identity.Perfil;
using Anjoz.Identity.Application.Filtros.Base;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Infrastructure.Extensoes;

namespace Anjoz.Identity.Application.Filtros.Identity
{
    public class PerfilGeradorFiltro : GeradorFiltro<Perfil, PerfilFiltroDto>
    {
        private Expression<Func<Perfil, bool>> _expressao;
        private PerfilFiltroDto _filtro;

        protected override Expression<Func<Perfil, bool>> Gerar(PerfilFiltroDto filtro, Expression<Func<Perfil, bool>> expressao)
        {
            _filtro = filtro;
            _expressao = expressao;
            
            FiltrarPorId();
            FiltrarPorNome();

            return _expressao;
        }

        private void FiltrarPorId()
        {
            if (_filtro.Id.HasValue)
                _expressao = _expressao.And(lnq => lnq.Id == _filtro.Id.Value);
        }

        private void FiltrarPorNome()
        {
            if (string.IsNullOrEmpty(_filtro.Nome) == false)
                _expressao = _expressao.And(lnq => lnq.NormalizedName.Contains(_filtro.Nome.ToUpper()));
        }
    }
}
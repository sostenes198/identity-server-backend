using System;
using System.Linq.Expressions;
using Anjoz.Identity.Application.Dtos.Identity.Claim;
using Anjoz.Identity.Application.Filtros.Base;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Infrastructure.Extensoes;

namespace Anjoz.Identity.Application.Filtros.Identity
{
    public class ClaimGeradorFiltro : GeradorFiltro<Claim, ClaimFiltroDto>
    {
        private ClaimFiltroDto _filtro;
        private Expression<Func<Claim, bool>> _expressao;

        protected override Expression<Func<Claim, bool>> Gerar(ClaimFiltroDto filtro, Expression<Func<Claim, bool>> expressao)
        {
            _filtro = filtro;
            _expressao = expressao;
            
            FiltrarPorId();
            FiltrarPorValor();
            FiltrarPorDescricao();

            return _expressao;
        }
        
        private void FiltrarPorId()
        {
            if (_filtro.Id != default)
                _expressao = _expressao.And(lnq => lnq.Id == _filtro.Id);
        }

        private void FiltrarPorValor()
        {
            if (string.IsNullOrWhiteSpace(_filtro.Valor) == false)
                _expressao = _expressao.And(lnq => lnq.ValorNormalizado.Contains(_filtro.Valor.ToUpperInvariant()));
        }

        private void FiltrarPorDescricao()
        {
            if (string.IsNullOrWhiteSpace(_filtro.Descricao) == false)
                _expressao = _expressao.And(lnq => lnq.DescricaNormalizada.Contains(_filtro.Descricao.ToUpperInvariant()));
        }
    }
}
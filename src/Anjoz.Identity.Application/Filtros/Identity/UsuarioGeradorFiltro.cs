using System;
using System.Linq.Expressions;
using Anjoz.Identity.Application.Dtos.Identity.Usuario;
using Anjoz.Identity.Application.Filtros.Base;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Infrastructure.Extensoes;

namespace Anjoz.Identity.Application.Filtros.Identity
{
    public class UsuarioGeradorFiltro : GeradorFiltro<Usuario, UsuarioFiltroDto>
    {
        private UsuarioFiltroDto _filtro;
        private Expression<Func<Usuario, bool>> _expressao;

        protected override Expression<Func<Usuario, bool>> Gerar(UsuarioFiltroDto filtro, Expression<Func<Usuario, bool>> expressao)
        {
            _filtro = filtro;
            _expressao = expressao;

            FiltrarPorId();
            FiltrarPorNome();
            FiltrarPorLogin();
            FiltrarPorEmail();
            FiltrarPorTelefone();
            FiltrarPorCodigoEquipe();

            return _expressao;
        }

        private void FiltrarPorId()
        {
            if (_filtro.Id != default)
                _expressao = _expressao.And(lnq => lnq.Id == _filtro.Id);
        }

        private void FiltrarPorNome()
        {
            if (_filtro.Nome != default)
                _expressao = _expressao.And(lnq => lnq.NormalizedUserName.Contains(_filtro.Nome.ToUpper()));
        }

        private void FiltrarPorLogin()
        {
            if (_filtro.Login != default)
                _expressao = _expressao.And(lnq => lnq.LoginNormalizado.Contains(_filtro.Login.ToUpper()));
        }

        private void FiltrarPorEmail()
        {
            if (_filtro.Email != default)
                _expressao = _expressao.And(lnq => lnq.NormalizedEmail.Contains(_filtro.Email.ToUpper()));
        }

        private void FiltrarPorTelefone()
        {
            if (_filtro.Telefone != default)
                _expressao = _expressao.And(lnq => lnq.PhoneNumber.Contains(_filtro.Telefone));
        }

        private void FiltrarPorCodigoEquipe()
        {
            if (_filtro.CodigoEquipe.HasValue)
                _expressao = _expressao.And(lnq => lnq.CodigoEquipe == _filtro.CodigoEquipe);
        }
    }
}
using Anjoz.Identity.Domain.Contratos.Validadores;
using Anjoz.Identity.Domain.Validadores.Base;
using Anjoz.Identity.Domain.VO;
using FluentValidation;

namespace Anjoz.Identity.Domain.Validadores.Identity.Usuario
{
    public class UsuarioAlteracaoSenhaVoCustomValidator : ServiceValidator<UsuarioAlteracaoSenhaVo>, ICustomServiceValidator<UsuarioAlteracaoSenhaVo>
    {
        public UsuarioAlteracaoSenhaVoCustomValidator()
        {
            RuleFor(lnq => lnq.Id)
                .NotEmpty();

            RuleFor(lnq => lnq.SenhaAtual)
                .NotEmpty();

            RuleFor(lnq => lnq.NovaSenha)
                .NotEmpty()
                .Equal(lnq => lnq.ConfirmacaoSenha)
                .Configure(lnq => lnq.CascadeMode = CascadeMode.StopOnFirstFailure);

            RuleFor(lnq => lnq.ConfirmacaoSenha)
                .NotEmpty();
        }
    }
}
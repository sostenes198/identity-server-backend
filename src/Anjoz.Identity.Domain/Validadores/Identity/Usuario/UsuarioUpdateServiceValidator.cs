using Anjoz.Identity.Domain.Configuracoes;
using Anjoz.Identity.Domain.Contratos.Validadores;
using Anjoz.Identity.Domain.Validadores.Base;
using FluentValidation;

namespace Anjoz.Identity.Domain.Validadores.Identity.Usuario
{
    public class UserUpdateServiceServiceValidator : ServiceValidator<Entidades.Identity.Usuario>, IUpdateServiceValidator<Entidades.Identity.Usuario>
    {
        public UserUpdateServiceServiceValidator()
        {
            RuleFor(lnq => lnq.Id)
                .NotEmpty();

            RuleFor(lnq => lnq.UserName)
                .NotEmpty()
                .MaximumLength(ConfiguracaoAplicacao.TamanhoMaximoCamposString);
            
            RuleFor(lnq => lnq.Login)
                .NotEmpty()
                .MaximumLength(ConfiguracaoAplicacao.TamanhoMaximoCamposString);
            
            RuleFor(lnq => lnq.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(ConfiguracaoAplicacao.TamanhoMaximoCamposString);
            
            RuleFor(lnq => lnq.PhoneNumber)
                .MaximumLength(ConfiguracaoAplicacao.TamanhoMaximoCamposString);

        }
    }
}
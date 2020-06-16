using Anjoz.Identity.Domain.Configuracoes;
using Anjoz.Identity.Domain.Contratos.Validadores;
using Anjoz.Identity.Domain.Validadores.Base;
using FluentValidation;

namespace Anjoz.Identity.Domain.Validadores.Identity.Usuario
{
    public class UsuarioSaveServiceValidator : ServiceValidator<Entidades.Identity.Usuario>, ISaveServiceValidator<Entidades.Identity.Usuario>
    {
        public UsuarioSaveServiceValidator()
        {
            RuleFor(lnq => lnq.UserName)
                .NotEmpty()
                .MaximumLength(ConfiguracaoAplicacao.TamanhoMaximoCamposString);
            
            RuleFor(lnq => lnq.Login)
                .NotEmpty()
                .MaximumLength(ConfiguracaoAplicacao.TamanhoMaximoCamposString);
            
            RuleFor(lnq => lnq.PasswordHash)
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
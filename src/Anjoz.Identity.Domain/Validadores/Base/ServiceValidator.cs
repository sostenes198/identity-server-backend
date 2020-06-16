using System.Linq;
using Anjoz.Identity.Domain.Contratos.Validadores;
using Anjoz.Identity.Domain.Excecoes;
using FluentValidation;

namespace Anjoz.Identity.Domain.Validadores.Base
{
    public class ServiceValidator<TEntidade> : AbstractValidator<TEntidade>, IServiceValidator<TEntidade>
        where TEntidade : class
    {
        public virtual void ValidarEntidade(TEntidade entidade)
        {
            var resultadoValidacao = Validate(entidade);

            if (resultadoValidacao.IsValid == false)
            {
                var mensagensErrors = resultadoValidacao.Errors.Select(lnq => lnq.ErrorMessage).ToList();
                throw new BusinessException(mensagensErrors);
            }
        }
    }
}
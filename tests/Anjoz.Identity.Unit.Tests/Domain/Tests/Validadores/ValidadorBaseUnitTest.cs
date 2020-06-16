using Anjoz.Identity.Domain.Contratos.Validadores;
using Anjoz.Identity.Utils.Tests.Extensoes;
using FluentAssertions;

namespace Anjoz.Identity.Unit.Tests.Domain.Tests.Validadores
{
    public abstract class ValidadorBaseUnitTest
    {
        protected void InvocarValidacao<TEntidade>(IServiceValidator<TEntidade> validador, TEntidade usuario, int quantidadeErro = 1)
            where TEntidade : class
        {
            validador.Invoking(lnq => lnq.ValidarEntidade(usuario))
                .Should()
                .ValidarExcecaoPropriedade(quantidadeErro);
        }
    }
}
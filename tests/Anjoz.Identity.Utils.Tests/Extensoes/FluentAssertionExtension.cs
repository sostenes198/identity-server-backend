using System.Linq;
using Anjoz.Identity.Domain.Excecoes;
using FluentAssertions;
using FluentAssertions.Specialized;

namespace Anjoz.Identity.Utils.Tests.Extensoes
{
    public static class FluentAssertionExtension
    {
        public static void ValidarExcecaoPropriedade<T>(this GenericAsyncFunctionAssertions<T> assertion, int quantitadeErros = 1,
            string mensagem = default)
        {
            ValidarPropriedade(assertion.Throw<BusinessException>(), quantitadeErros, mensagem);
        }
        
        public static void ValidarExcecaoPropriedade(this ActionAssertions assertion, int quantitadeErros = 1,
            string mensagem = default)
        {
            ValidarPropriedade(assertion.Throw<BusinessException>(), quantitadeErros, mensagem);
        }

        public static void ValidarExcecaoPropriedade(this NonGenericAsyncFunctionAssertions assertion, int quantitadeErros = 1,
            string mensagem = default)
        {
            ValidarPropriedade(assertion.Throw<BusinessException>(), quantitadeErros, mensagem);
        }

        private static void ValidarPropriedade(ExceptionAssertions<BusinessException> resultado, int quantitadeErros,
            string mensagem)
        {
            var message = $"{resultado.And.Errors.Aggregate((s, s1) => $"{s}\n{s1}")}";
            
            resultado.And.Errors.Count.Should().Be(quantitadeErros, message);
            
            if (mensagem != default)
                resultado.And.Errors.Should().Contain(mensagem);
        }
    }
}
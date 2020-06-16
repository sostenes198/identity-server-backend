using System;
using System.Collections.Generic;
using Anjoz.Identity.Domain.Excecoes;
using FluentAssertions;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Domain.Tests.Execoes
{
    public class NegocioExceptionUnitTest
    {
        [Fact]
        public void Deve_Gerar_Lista_De_Negocio_Exception()
        {
            var exception = new BusinessException(new List<string> {"Mensagem de Erro1", "Mensagem de Erro2"});

            exception.Errors.Should().NotBeNull().And.HaveCount(2);
        }

        [Fact]
        public void Deve_Gerar_Lista_De_Negocio_Exception_Com_Inner_Exception()
        {
            var exception = new BusinessException(new List<string> {"Mensagem de Erro1", "Mensagem de Erro2"}, new Exception());

            exception.Errors.Should().NotBeNull().And.HaveCount(2);
            exception.InnerException.Should().NotBeNull();
        }
        
        [Fact]
        public void Deve_Gerar_Negocio_Exception()
        {
            var exception = new BusinessException("Mensagem de Erro");

            exception.Message.Should().BeEquivalentTo("Mensagem de Erro");
            exception.Errors.Should().NotBeNull().And.HaveCount(1);
        }

        [Fact]
        public void Deve_Gerar_Negocio_Exception_Com_Inner_Exception()
        {
            var exception = new BusinessException("Mensagem de Erro", new Exception());

            exception.Message.Should().BeEquivalentTo("Mensagem de Erro");
            exception.Errors.Should().NotBeNull().And.HaveCount(1);
            exception.InnerException.Should().NotBeNull();
        }
    }
}
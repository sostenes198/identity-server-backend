using System.Collections.Generic;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Entidades.Paginacao;
using Anjoz.Identity.Domain.Extensoes.Paginacao;
using FluentAssertions;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Domain.Tests.Extensoes.Paginacao
{
    public class PagedListExtensionsUniTests
    {
        [Fact]
        public void Deve_Tranformar_Listar_Em_Lista_Paginada()
        {
            var resultado = new List<Claim> {new Claim {Id = 1, Descricao = "Teste1"}, new Claim {Id = 2, Descricao = "Teste2"}}.ToPagedList();
            var resultadoEsperado = new PagedList<Claim>(new List<Claim> {new Claim {Id = 1, Descricao = "Teste1"}, new Claim {Id = 2, Descricao = "Teste2"}},
                1, 9999, 1);
            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public void Deve_Transforma_Listar_Em_Lista_Paginada_Utilizando_Informacoes_Paginacao()
        {
            var resultado = new List<Claim> {new Claim {Id = 1, Descricao = "Teste1"}, new Claim {Id = 2, Descricao = "Teste2"}}.ToPagedList(new PagedParam(5, 1));
            var resultadoEsperado = new PagedList<Claim>(new List<Claim> {new Claim {Id = 1, Descricao = "Teste1"}, new Claim {Id = 2, Descricao = "Teste2"}},
                1, 5, 1);
            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }
    }
}
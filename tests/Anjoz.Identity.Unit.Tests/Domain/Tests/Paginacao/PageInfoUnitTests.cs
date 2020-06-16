using Anjoz.Identity.Domain.Entidades.Paginacao;
using FluentAssertions;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Domain.Tests.Paginacao
{
    public class PageInfoUnitTests
    {
        [Fact]
        public void Deve_Inicializar_PageInfo()
        {
            var resultado = new PageInfo();
            var resultadoEsperado = new PageInfo
            {
                PageNumber = 0,
                PageSize = 0,
                TotalPages = 0
            };

            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public void Deve_Inicializar_PageInfo_Com_PagedParams()
        {
            var resultado = new PageInfo(new PagedParam {PageNumber = 1, PageSize = 5, TotalPages = 10});
            var resultadoEsperado = new PageInfo
            {
                PageNumber = 1,
                PageSize = 5,
                TotalPages = 10
            };

            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Theory]
        [InlineData(1, 5, 1, false)]
        [InlineData(1, 5, 2, true)]
        [InlineData(2, 5, 2, false)]
        public void Deve_Validar_Se_Existe_Proxima_Pagina(int pageNumber, int pageSize, int totalPages, bool resultadoEsperado)
        {
            var resultado = new PageInfo
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
            resultado.HasNext.Should().Be(resultadoEsperado);
        }

        [Theory]
        [InlineData(1, 5, 1, false)]
        [InlineData(1, 5, 2, false)]
        [InlineData(2, 5, 2, true)]
        public void Deve_Validar_Se_Existe_Pagina_Anterior(int pageNumber, int pageSize, int totalPages, bool resultadoEsperado)
        {
            var resultado = new PageInfo
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
            resultado.HasPrevious.Should().Be(resultadoEsperado);
        }
    }
}
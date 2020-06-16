using System.Linq;
using Anjoz.Identity.Application.Dtos.Identity.Claim;
using Anjoz.Identity.Application.Filtros.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Infrastructure.Interfaces.Filter;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using FluentAssertions;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Application.Tests.Filtros.Identity
{
    public class ClaimGeradorFiltroUnitTest
    {
        private readonly IGeradorFiltro<Claim, ClaimFiltroDto> _geradorFiltro;

        public ClaimGeradorFiltroUnitTest()
        {
            _geradorFiltro = new ClaimGeradorFiltro();
        }
        
        [Fact]
        public void Deve_Filtrar_Claims_Por_Id()
        {
            var filtro = new ClaimFiltroDto {Id = 1};

            var where = _geradorFiltro.Gerar(filtro);

            var claims = ClaimUtils.Claims.Where(where.Compile());

            claims.Should().NotBeNull().And.HaveCount(1);
        }

        [Fact]
        public void Deve_Filtrar_Claims_Por_Valor()
        {
            var filtro = new ClaimFiltroDto {Valor = "value1"};

            var where = _geradorFiltro.Gerar(filtro);

            var claims = ClaimUtils.Claims.Where(where.Compile());

            claims.Should().NotBeNull().And.HaveCount(2);   
        }

        [Fact]
        public void Deve_Filtrar_Claim_Por_Descricao()
        {
            var filtro = new ClaimFiltroDto {Descricao = "descricao1"};

            var where = _geradorFiltro.Gerar(filtro);

            var claims = ClaimUtils.Claims.Where(where.Compile());

            claims.Should().NotBeNullOrEmpty().And.HaveCount(2);
        }
    }
}
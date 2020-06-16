using System.Linq;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Servicos.Identity;
using Anjoz.Identity.Unit.Tests.Domain.Fixtures.Crud;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using FluentAssertions;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Domain.Tests.Servicos.Crud
{
    public class CrudServiceUnitTest : IClassFixture<CrudServiceFixture>
    {
        private readonly IClaimService _service;

        public CrudServiceUnitTest(CrudServiceFixture fixture)
        {
            _service = new ClaimService(fixture.InicializarCrudRepository(), fixture.DomainValidator<Claim>());
        }

        [Fact]
        public async Task Deve_Criar()
        {
            var claim = new Claim {Valor = "value", Descricao = "Descricao"};
            var resultadoEsperado = new Claim {Id = 1, Valor = "VALUE", ValorNormalizado = "VALUE", Descricao = "Descricao"};

            await _service.CriarAsync(claim);

            claim.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task Deve_Atualizar()
        {
            var resultadoEsperado = new Claim {Id = 1, Valor = "value_update", ValorNormalizado = "VALUE_UPDATE", Descricao = "Descricao1"};
            var claim = ClaimUtils.Claims.First();
            claim.Valor = "value_update";

            await _service.AtualizarAsync(claim);

            claim.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public void Deve_Deletar()
        {
            _service.Invoking(async lnq => await lnq.ExcluirAsync(1))
                .Should().NotThrow();
        }

        [Fact]
        public async Task Deve_Obter_Por_Id()
        {
            var claimEsperada = ClaimUtils.Claims.FirstOrDefault(lnq => lnq.Id == 1);
            var claim = await _service.ObterPorIdAsync(1);
            
            claim.Should().BeEquivalentTo(claimEsperada);
        }

        [Fact]
        public async Task Deve_Listar_Todos()
        {
            var claims = await _service.ListarPorAsync();

            claims.Should().BeEquivalentTo(ClaimUtils.Claims);
        }

        [Fact]
        public async Task Deve_Listar_Todos_Com_Filtro()
        {
            var claimsExpected = ClaimUtils.Claims.Where(lnq => lnq.Valor.Contains("1"));
            var claims = await _service.ListarPorAsync(lnq => lnq.Valor.Contains("1"));
            
            claims.Should().BeEquivalentTo(claimsExpected);
        }
    }
}
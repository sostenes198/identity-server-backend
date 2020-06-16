using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Excecoes;
using Anjoz.Identity.Domain.Servicos.Identity;
using Anjoz.Identity.Unit.Tests.Domain.Fixtures;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using FluentAssertions;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Domain.Tests.Servicos
{
    public class EntidadeVinculoServiceUnitTest : IClassFixture<EntidadeVinculoServiceFixture>
    {
        private readonly UsuarioClaimService _usuarioClaimService;

        public EntidadeVinculoServiceUnitTest(EntidadeVinculoServiceFixture fixture)
        {
            _usuarioClaimService = new UsuarioClaimService(fixture.InitializeUserClaimRepository(), fixture.InitializeClaimService(),
                fixture.DomainValidator<UsuarioClaim>());
        }

        [Fact]
        public void Deve_Popular_Claim_Do_Usuario()
        {
            var resultadoEsperado = new List<UsuarioClaim>
            {
                new UsuarioClaim {UserId = 1, ClaimId = 1},
                new UsuarioClaim {UserId = 1, ClaimId = 2},
                new UsuarioClaim {UserId = 1, ClaimId = 3},
                new UsuarioClaim {UserId = 1, ClaimId = 4},
                new UsuarioClaim {UserId = 1, ClaimId = 5}
            };
            var resultado = _usuarioClaimService.PopularVinculos(1, new[] {1, 2, 3, 4, 5});

            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task Deve_Listar_Todas_Claims_Usuario()
        {
            var resultadoEsperado = new List<UsuarioClaim>
            {
                new UsuarioClaim {UserId = 1, ClaimId = 1},
                new UsuarioClaim {UserId = 1, ClaimId = 2},
                new UsuarioClaim {UserId = 1, ClaimId = 3},
                new UsuarioClaim {UserId = 1, ClaimId = 4},
                new UsuarioClaim {UserId = 1, ClaimId = 5}
            };

            var resultado = await _usuarioClaimService.ListarTodosVinculosEntidade(1);

            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task Deve_Listar_Todas_Claims()
        {
            var resultadoEsperado = ClaimUtils.Claims.Where(lnq => new[] {1, 2, 3, 4, 5}.Contains(lnq.Id)).Select(lnq => lnq.Id);

            var resultado = await _usuarioClaimService.ListarTodosVinculos(new[] {1, 2, 3, 4, 5});

            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public void Deve_Remover_Todos_Vinculos()
        {
            _usuarioClaimService.Invoking(async lnq => await lnq.RemoverTodosVinculos(1))
                .Should()
                .NotThrow();
        }

        [Fact]
        public void Deve_Adicionar_Vinculo()
        {
            _usuarioClaimService.Invoking(async lnq => await lnq.AdicionarVinculo(1, new[] {1, 2, 3, 4}))
                .Should()
                .NotThrow();
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Adicionar_Vinculo_E_Nao_Existir_Todos_Vinculos()
        {
            _usuarioClaimService.Invoking(async lnq => await lnq.AdicionarVinculo(1, new[] {1, 2, 10, 20, 30}))
                .Should()
                .Throw<BusinessException>();
        }

        [Fact]
        public void Deve_Atualizar_Vinculos()
        {
            _usuarioClaimService.Invoking(async lnq => await lnq.AtualizarVinculos(1, new[] {1, 2, 3, 4}))
                .Should()
                .NotThrow();
        }

        [Fact]
        public void Deve_Atualizar_Vinculos_RemoverTodos()
        {
            _usuarioClaimService.Invoking(async lnq => await lnq.AtualizarVinculos(1, new int[0]))
                .Should()
                .NotThrow();
        }
    }
}
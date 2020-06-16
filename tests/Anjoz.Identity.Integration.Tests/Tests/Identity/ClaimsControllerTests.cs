using System.Collections.Generic;
using System.Threading.Tasks;
using Anjoz.Identity.Application.Dtos.Identity.Claim;
using Anjoz.Identity.Application.Dtos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Entidades.Paginacao;
using Anjoz.Identity.Integration.Tests.Extensions;
using Anjoz.Identity.Integration.Tests.Fixtures.Base;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Anjoz.Identity.Integration.Tests.Tests.Identity
{
    public class ClaimsControllerTests : IClassFixture<BaseIntegrationTestFixture>
    {
        private readonly BaseIntegrationTestFixture _fixture;
        private readonly IClaimService _claimService;

        private readonly string _baseClaimsUrl;

        public ClaimsControllerTests(BaseIntegrationTestFixture fixture)
        {
            _fixture = fixture;
            _claimService = fixture.ServiceProvider.GetService<IClaimService>();
            _baseClaimsUrl = $"{fixture.BaseUrl}/claims";
        }

        [Fact]
        public Task Deve_Listar_Todas_Claims()
        {
            var claims = new List<Claim>
            {
                new Claim {Valor = "Valor1", Descricao = "Valor1"},
                new Claim {Valor = "Valor2", Descricao = "Valor2"},
                new Claim {Valor = "Valor3", Descricao = "Valor3"},
                new Claim {Valor = "Valor4", Descricao = "Valor4"},
                new Claim {Valor = "Valor5", Descricao = "Valor5"},
            };

            return _fixture.ListarTodosRegistrosAsync<Claim, ClaimDto>(
                _baseClaimsUrl,
                _fixture.CriarRegistros(claims, _claimService),
                async () => await _claimService.ListarPorAsync()
            );
        }

        [Fact]
        public Task Deve_Listar_Claims_Com_Filtro()
        {
            var claims = new List<Claim>
            {
                new Claim {Valor = "BBValor1", Descricao = "BBValor1"},
                new Claim {Valor = "BBValor2", Descricao = "BBValor2"},
                new Claim {Valor = "BBValor3", Descricao = "BBValor3"},
                new Claim {Valor = "BBValor4", Descricao = "BBValor4"},
            };
            var claimFiltroDto = new PageListFiltroDto<ClaimFiltroDto> {Filtro = new ClaimFiltroDto {Valor = "BBValor"}, PagedParam = new PagedParamFiltroDto {PageNumber = 1, PageSize = 2}};

            return _fixture.ListarTodosRegistrosComFiltroAsync<Claim, PageListFiltroDto<ClaimFiltroDto>, ClaimDto>(
                _baseClaimsUrl,
                claimFiltroDto,
                _fixture.CriarRegistros(claims, _claimService),
                async () => await _claimService.ListarPorAsync(lnq => lnq.ValorNormalizado.Contains("BBVALOR"), default, new PagedParam(2, 1))
            );
        }

        [Fact]
        public Task Deve_Obter_Claim()
        {
            var claim = new Claim {Valor = "Consulta Claim", Descricao = "Consulta Claim"};
            return _fixture.ObterRegistroAsync<Claim, ClaimDto>(
                _fixture.CriarRegistros(claim, _claimService),
                () => $"{_baseClaimsUrl}/{claim.Id}",
                (response) => _claimService.ObterPorIdAsync(response.Id)
            );
        }


        [Fact]
        public Task Deve_Criar_Claim()
        {
            var claimDto = new ClaimCriarDto {Valor = "Teste", Descricao = "Teste"};
            return _fixture.CriarAsync<Claim, ClaimCriarDto, ClaimDto>(
                _baseClaimsUrl,
                claimDto,
                (resultadoResponse) => _claimService.ObterPorIdAsync(resultadoResponse.Id));
        }

        [Fact]
        public Task Deve_Atualizar_Claim()
        {
            var claim = new Claim {Valor = "Teste Criação", Descricao = "Teste Criação"};
            return _fixture.AtualizarAsync<Claim, ClaimAtualizarDto, ClaimDto>(
                _baseClaimsUrl,
                _fixture.CriarRegistros(claim, _claimService),
                () => new ClaimAtualizarDto {Id = claim.Id, Valor = "Atualizado Valor", Descricao = "Descrição atualizada"},
                (resultadoResponse) => _claimService.ObterPorIdAsync(resultadoResponse.Id));
        }

        [Fact]
        public Task Deve_Excluir_Claim()
        {
            var claim = new Claim {Valor = "Teste Criação para exclusão", Descricao = "Teste Criação para exclusão"};
            return _fixture.ExcluirAsync<Claim, int>(
                _fixture.CriarRegistros(claim, _claimService),
                () => $"{_baseClaimsUrl}/{claim.Id}",
                (id) => _claimService.ObterPorIdAsync(id)
            );
        }
    }
}
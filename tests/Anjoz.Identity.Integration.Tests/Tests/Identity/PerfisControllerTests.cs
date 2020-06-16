using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anjoz.Identity.Application.Dtos.Base;
using Anjoz.Identity.Application.Dtos.Identity.Claim;
using Anjoz.Identity.Application.Dtos.Identity.Perfil;
using Anjoz.Identity.Application.Dtos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Entidades.Paginacao;
using Anjoz.Identity.Integration.Tests.Extensions;
using Anjoz.Identity.Integration.Tests.Fixtures.Identity;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Anjoz.Identity.Integration.Tests.Tests.Identity
{
    public class PerfisControllerTests : IClassFixture<PerfisControllerFixture>
    {
        private readonly string _baseUrlPerfil;
        private readonly PerfisControllerFixture _fixture;
        private readonly IPerfilService _perfilService;
        private readonly IClaimService _claimService;
        private readonly IPerfilClaimService _perfilClaimVinculoService;

        public PerfisControllerTests(PerfisControllerFixture fixture)
        {
            _fixture = fixture;
            _baseUrlPerfil = $"{fixture.BaseUrl}/perfis";
            _perfilService = fixture.ServiceProvider.GetService<IPerfilService>();
            _claimService = fixture.ServiceProvider.GetService<IClaimService>();
            _perfilClaimVinculoService = fixture.ServiceProvider.GetService<IPerfilClaimService>();
        }

        [Fact]
        public Task Deve_Listar_Todos()
        {
            var perfis = new List<Perfil>
            {
                new Perfil {Name = "Teste1"},
                new Perfil {Name = "Teste2"},
                new Perfil {Name = "Teste3"},
                new Perfil {Name = "Teste4"},
            };

            return _fixture.ListarTodosRegistrosAsync<Perfil, PerfilDto>(
                _baseUrlPerfil,
                _fixture.CriarRegistros(perfis, _perfilService),
                async () => await _perfilService.ListarPorAsync()
            );
        }

        [Fact]
        public Task Deve_Listar_Por_Filtro()
        {
            var perfis = new List<Perfil>
            {
                new Perfil {Name = "AATeste1"},
                new Perfil {Name = "AATeste2"},
                new Perfil {Name = "AATeste3"},
                new Perfil {Name = "AATeste4"},
            };

            var perfilFiltroDto = new PageListFiltroDto<PerfilFiltroDto> {Filtro = new PerfilFiltroDto() {Nome = "AATest"}, PagedParam = new PagedParamFiltroDto {PageNumber = 2, PageSize = 2}};

            return _fixture.ListarTodosRegistrosComFiltroAsync<Perfil, PageListFiltroDto<PerfilFiltroDto>, PerfilDto>(
                _baseUrlPerfil,
                perfilFiltroDto,
                _fixture.CriarRegistros(perfis, _perfilService),
                async () => await _perfilService.ListarPorAsync(lnq => lnq.Name.Contains("AATest"), default, new PagedParam(2, 2))
            );
        }

        [Fact]
        public Task Deve_Listar_Por_Id()
        {
            var perfil = new Perfil {Name = "Teste5"};

            return _fixture.ObterRegistroAsync<Perfil, PerfilDto>(
                _fixture.CriarRegistros(perfil, _perfilService),
                () => $"{_baseUrlPerfil}/{perfil.Id}",
                (response) => _perfilService.ObterPorIdAsync(response.Id)
            );
        }

        [Fact]
        public Task Deve_Listar_Por_Nome()
        {
            var perfil = new Perfil {Name = "Teste6"};
            return _fixture.ObterRegistroAsync<Perfil, PerfilDto>(
                _fixture.CriarRegistros(perfil, _perfilService),
                () => $"{_baseUrlPerfil}/nome/{perfil.Name}",
                (response) => _perfilService.ObterPorIdAsync(response.Id)
            );
        }

        [Fact]
        public Task Deve_Listar_Claims()
        {
            var claims = _fixture.Claims;

            _fixture.GerarRegistros(claims, _claimService);

            var claimsId = new HashSet<int>(claims.Select(lnq => lnq.Id).ToArray());

            var perfilClaim = new List<PerfilClaim>();
            foreach (var claimId in claimsId)
            {
                perfilClaim.Add(new PerfilClaim {ClaimId = claimId});
            }

            var perfil = new Perfil {Name = "PerfilClaimTeste", PerfisClaims = perfilClaim};


            return _fixture.ObterRegistroAsync<IPagedList<Claim>, PagedListDto<ClaimDto>>(
                _fixture.CriarRegistros(perfil, _perfilService),
                () => $"{_baseUrlPerfil}/{perfil.Id}/claims",
                async (response) => await _claimService.ListarPorAsync(lnq => response.Items.Select(t => t.Id).Contains(lnq.Id)));
        }

        [Fact]
        public async Task Deve_Criar()
        {
            var claims = _fixture.PerfilClaims;

            _fixture.GerarRegistros(claims, _claimService);

            var claimsId = new HashSet<int>(claims.Select(lnq => lnq.Id).ToArray());

            var perfilCriarDto = new PerfilCriarDto {Nome = "Teste7", ClaimsId = claimsId};

            await _fixture.CriarAsync<Perfil, PerfilCriarDto, PerfilDto>(
                _baseUrlPerfil,
                perfilCriarDto,
                (response) => _perfilService.ObterPorIdAsync(response.Id)
            );

            var perfil = await _perfilService.ObterPorNomeAsync(perfilCriarDto.Nome);
            var claimsIdUsuario = (await _perfilClaimVinculoService.ListarPorAsync(lnq => lnq.RoleId == perfil.Id)).Select(lnq => lnq.ClaimId);

            claimsId.Should().BeEquivalentTo(claimsIdUsuario);
        }

        [Fact]
        public async Task Deve_Atualizar()
        {
            var claims = _fixture.ClaimsAtualizar;

            _fixture.GerarRegistros(claims, _claimService);

            var claimsId = new HashSet<int>(claims.Select(lnq => lnq.Id).ToArray());

            var perfil = new Perfil {Name = "Teste7"};

            await _fixture.AtualizarAsync<Perfil, PerfilAtualizarDto, PerfilDto>(
                _baseUrlPerfil,
                _fixture.CriarRegistros(perfil, _perfilService),
                () => new PerfilAtualizarDto {Id = perfil.Id, Nome = "TESTANDO", ClaimsId = claimsId},
                (response) => _perfilService.ObterPorIdAsync(response.Id)
            );

            var claimsIdUsuario = (await _perfilClaimVinculoService.ListarPorAsync(lnq => lnq.RoleId == perfil.Id)).Select(lnq => lnq.ClaimId);

            claimsId.Should().BeEquivalentTo(claimsIdUsuario);
        }

        [Fact]
        public Task Deve_Excluir_Usuario()
        {
            var perfil = new Perfil {Name = "Teste8"};

            return _fixture.ExcluirAsync<Perfil, int>(
                _fixture.CriarRegistros(perfil, _perfilService),
                () => $"{_baseUrlPerfil}/{perfil.Id}",
                (response) => _perfilService.ObterPorIdAsync(response)
            );
        }
    }
}
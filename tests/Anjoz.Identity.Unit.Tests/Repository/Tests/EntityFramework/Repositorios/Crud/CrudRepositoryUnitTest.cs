using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Repositorios.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Entidades.Paginacao;
using Anjoz.Identity.Unit.Tests.Repository.Fixtures.Crud;
using Anjoz.Identity.Utils.Tests.Fakers.Identity;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Repository.Tests.EntityFramework.Repositorios.Crud
{
    public class CrudRepositoryUnitTest : IClassFixture<CrudUnitTestRepositoryFixture>
    {
        private readonly CrudUnitTestRepositoryFixture _fixture;
        private readonly IClaimRepository _crudRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public CrudRepositoryUnitTest(CrudUnitTestRepositoryFixture fixture)
        {
            _fixture = fixture;
            _crudRepository = fixture.ServiceProvider.GetService<IClaimRepository>();
            _usuarioRepository = fixture.ServiceProvider.GetService<IUsuarioRepository>();
        }

        [Fact]
        public async Task Deve_Criar_Entidade()
        {
            var claim = await _fixture.CriarEntidadeAsync(_crudRepository, new ClaimFaker());

            var resultadoEsperado = await _crudRepository.ObterPorIdAsync(claim.Id);

            claim.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task Deve_Criar_Entidades()
        {
            var claim = (await _fixture.CriarEntidadeAsync(_crudRepository, new ClaimFaker(), 5)).ToList();

            var ids = claim.Select(lnq => lnq.Id);

            var resultadoEsperado = await _crudRepository.ListarPorAsync(lnq => ids.Contains(lnq.Id));

            claim.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task Deve_Atualizar()
        {
            var claim = await _fixture.CriarEntidadeAsync(_crudRepository, new ClaimFaker());

            claim.Valor = "UPDATE_VALUE";

            await _crudRepository.AtualizarAsync(claim);

            var resultadoEsperado = await _crudRepository.ObterPorIdAsync(claim.Id);

            claim.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task Deve_Deletar_Entidade()
        {
            var claim = await _fixture.CriarEntidadeAsync(_crudRepository, new ClaimFaker());

            await _crudRepository.ExcluirAsync(claim);

            var resultado = await _crudRepository.ObterPorIdAsync(claim.Id);

            resultado.Should().BeNull();
        }

        [Fact]
        public async Task Deve_Deletar_Entidades()
        {
            var claims = (await _fixture.CriarEntidadeAsync(_crudRepository, new ClaimFaker(), 5)).ToList();

            var ids = claims.Select(lnq => lnq.Id);

            await _crudRepository.ExcluirAsync(claims);

            var resultado = await _crudRepository.ListarPorAsync(lnq => ids.Contains(lnq.Id));

            resultado.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task Deve_Obter_Por_Id()
        {
            var claim = await _fixture.CriarEntidadeAsync(_crudRepository, new ClaimFaker());

            var resultado = await _crudRepository.ObterPorIdAsync(claim.Id);

            claim.Should().BeEquivalentTo(resultado);
        }

        [Fact]
        public async Task Deve_Obter_Por_Id_Com_Include_De_ClaimUsuario()
        {
            var claim = await _fixture.CriarEntidadeAsync(_crudRepository, new ClaimFaker());
            var usuario = new UsuarioFaker().Generate();
            usuario.UsuariosClaims = new List<UsuarioClaim>
            {
                new UsuarioClaim {ClaimId = claim.Id}
            };

            await _usuarioRepository.CriarAsync(usuario, "Abc@123");

            var resultado = await _crudRepository.ObterPorIdAsync(claim.Id);

            resultado.Should().BeEquivalentTo(claim, opt => opt.Excluding(prop => prop.UsuariosClaims));

            resultado.UsuariosClaims.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Deve_Listar_Todos()
        {
            await _fixture.CriarEntidadeAsync(_crudRepository, new ClaimFaker());

            var resultado = await _crudRepository.ListarPorAsync();

            resultado.Should().NotBeEmpty().And.Should().NotBeNull();
        }

        [Fact]
        public async Task Deve_Listar_Todos_Com_Filtro()
        {
            var claim = await _fixture.CriarEntidadeAsync(_crudRepository, new ClaimFaker());

            var resultado = (await _crudRepository.ListarPorAsync(lnq => lnq.Id == claim.Id)).ToList();

            resultado.Should().NotBeEmpty().And.Should().NotBeNull();

            resultado.First().Should().BeEquivalentTo(claim);
        }

        [Fact]
        public async Task Deve_Listar_Todos_Com_Include_De_ClaimUsuario()
        {
            var claim = await _fixture.CriarEntidadeAsync(_crudRepository, new ClaimFaker());
            var usuario = new UsuarioFaker().Generate();
            usuario.UsuariosClaims = new List<UsuarioClaim>
            {
                new UsuarioClaim {ClaimId = claim.Id}
            };

            await _usuarioRepository.CriarAsync(usuario, "Abc@123");

            var resultado = (await _crudRepository.ListarPorAsync(lnq => lnq.Id == claim.Id, new[] {nameof(Claim.UsuariosClaims)})).First();

            resultado.Should().BeEquivalentTo(claim, opt => opt.Excluding(prop => prop.UsuariosClaims));

            resultado.UsuariosClaims.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Deve_Listar_Paginado()
        {
            var quantidadeItems = 10;
            var claims = new List<Claim>();
            for (int i = 0; i < quantidadeItems; i++)
            {
                var claim = await _fixture.CriarEntidadeAsync(_crudRepository, new ClaimFaker());
                claims.Add(claim);
            }

            var pagedParams = new PagedParam(5, 1);
            var idClaims = claims.Select(lnq => lnq.Id);
            Expression<Func<Claim, bool>> filtro = lnq => idClaims.Contains(lnq.Id);

            var resultadoPrimeiraPagina = await _crudRepository.ListarPorAsync(filtro, default, pagedParams);
            resultadoPrimeiraPagina.Count.Should().Be(5);
            resultadoPrimeiraPagina.PageInfo.HasNext.Should().BeTrue();
            resultadoPrimeiraPagina.PageInfo.HasPrevious.Should().BeFalse();
            resultadoPrimeiraPagina.PageInfo.PageNumber.Should().Be(1);
            resultadoPrimeiraPagina.PageInfo.PageSize.Should().Be(5);
            resultadoPrimeiraPagina.PageInfo.TotalPages.Should().Be(2);
            
            
            var resultadoSegundaPagina = await _crudRepository.ListarPorAsync(filtro, default, pagedParams.Next());
            resultadoSegundaPagina.Count.Should().Be(5);
            resultadoSegundaPagina.PageInfo.HasNext.Should().BeFalse();
            resultadoSegundaPagina.PageInfo.HasPrevious.Should().BeTrue();
            resultadoSegundaPagina.PageInfo.PageNumber.Should().Be(2);
            resultadoSegundaPagina.PageInfo.PageSize.Should().Be(5);
            resultadoSegundaPagina.PageInfo.TotalPages.Should().Be(2);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Repositorios.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Entidades.Paginacao;
using Anjoz.Identity.Unit.Tests.Repository.Fixtures.Identity;
using Anjoz.Identity.Utils.Tests.Fakers.Identity;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Repository.Tests.EntityFramework.Repositorios.Identity
{
    public class PerfilRepositoryUnitTest : IClassFixture<PerfilRepositoryFixture>
    {
        private readonly PerfilRepositoryFixture _fixture;
        private readonly IPerfilRepository _perfilRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public PerfilRepositoryUnitTest(PerfilRepositoryFixture fixture)
        {
            _fixture = fixture;
            _perfilRepository = fixture.ServiceProvider.GetService<IPerfilRepository>();
            _usuarioRepository = fixture.ServiceProvider.GetService<IUsuarioRepository>();
        }

        [Fact]
        public async Task Deve_Criar_Perfil()
        {
            var resultadoEsperado = await GerarEValidarPerfil();
            var resultado = await _perfilRepository.ObterPorIdAsync(resultadoEsperado.Id);
            resultadoEsperado.Should().BeEquivalentTo(resultado);
        }

        [Fact]
        public async Task Deve_Ataulizar_Perfil()
        {
            var resultadoEsperado = await GerarEValidarPerfil();

            resultadoEsperado.Name = "teste_atualização";

            var resultadoAtualizar = await _perfilRepository.AtualizarAsync(resultadoEsperado);
            resultadoAtualizar.Succeeded.Should().BeTrue();

            var resultado = await _perfilRepository.ObterPorIdAsync(resultadoEsperado.Id);
            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task Deve_Excluir_Perfil()
        {
            var resultadoEsperado = await GerarEValidarPerfil();

            var resultadoExclusao = await _perfilRepository.ExcluirAsync(resultadoEsperado);
            resultadoExclusao.Succeeded.Should().BeTrue();

            var resultado = await _perfilRepository.ObterPorIdAsync(resultadoEsperado.Id);
            resultado.Should().BeNull();
        }

        [Fact]
        public async Task Deve_Obter_Perfil_Por_Id()
        {
            var resultadoEsperado = await GerarEValidarPerfil();

            var resultado = await _perfilRepository.ObterPorIdAsync(resultadoEsperado.Id);

            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task Deve_Obter_Perfil_Por_Nome()
        {
            var resultadoEsperado = await GerarEValidarPerfil();

            var resultado = await _perfilRepository.ObterPorNomeAsync(resultadoEsperado.Name);

            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task Deve_Obter_Perfil_Por_Nome_Com_Include_Usuario()
        {
            var usuario = new UsuarioFaker().Generate();
             await _usuarioRepository.CriarAsync(usuario, "Ab@$321");
        }

        [Fact]
        public async Task Deve_Listar_Todos_Perfis()
        {
            await GerarEValidarPerfil();

            var resultado = await _perfilRepository.ListarPorAsync();

            resultado.Should().NotBeNull();
        }
        
        [Fact]
        public async Task Deve_Listar_Todos_Perfis_Com_Filtro()
        {
            var resultadoEsperado = await GerarEValidarPerfil();

            var resultado = await _perfilRepository.ListarPorAsync(lnq => lnq.NormalizedName == resultadoEsperado.NormalizedName);

            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }
        
        [Fact]
        public async Task Deve_Listar_Paginado()
        {
            var quantidadeItems = 10;
            var perfis = new List<Perfil>();
            for (int i = 0; i < quantidadeItems; i++)
            {
                var perfil = await GerarEValidarPerfil();
                perfis.Add(perfil);
            }

            var pagedParams = new PagedParam(5, 1);
            var idPerfis = perfis.Select(lnq => lnq.Id);
            Expression<Func<Perfil, bool>> filtro = lnq => idPerfis.Contains(lnq.Id);

            var resultadoPrimeiraPagina = await _perfilRepository.ListarPorAsync(filtro, default, pagedParams);
            resultadoPrimeiraPagina.Count.Should().Be(5);
            resultadoPrimeiraPagina.PageInfo.HasNext.Should().BeTrue();
            resultadoPrimeiraPagina.PageInfo.HasPrevious.Should().BeFalse();
            resultadoPrimeiraPagina.PageInfo.PageNumber.Should().Be(1);
            resultadoPrimeiraPagina.PageInfo.PageSize.Should().Be(5);
            resultadoPrimeiraPagina.PageInfo.TotalPages.Should().Be(2);
            
            
            var resultadoSegundaPagina = await _perfilRepository.ListarPorAsync(filtro, default, pagedParams.Next());
            resultadoSegundaPagina.Count.Should().Be(5);
            resultadoSegundaPagina.PageInfo.HasNext.Should().BeFalse();
            resultadoSegundaPagina.PageInfo.HasPrevious.Should().BeTrue();
            resultadoSegundaPagina.PageInfo.PageNumber.Should().Be(2);
            resultadoSegundaPagina.PageInfo.PageSize.Should().Be(5);
            resultadoSegundaPagina.PageInfo.TotalPages.Should().Be(2);
        }

        private async Task<Perfil> GerarEValidarPerfil()
        {
            var (identityResult, resultadoEsperado) = await _fixture.GerarPerfilRandomico(_perfilRepository);
            identityResult.Succeeded.Should().BeTrue();
            return resultadoEsperado;
        }
    }
}
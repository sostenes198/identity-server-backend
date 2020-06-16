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
    public class UsuarioRepositoryUnitTest : IClassFixture<UsuarioRepositoryFixture>
    {
        private readonly UsuarioRepositoryFixture _fixture;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPerfilRepository _perfilRepository;

        public UsuarioRepositoryUnitTest(UsuarioRepositoryFixture fixture)
        {
            _fixture = fixture;
            _usuarioRepository = fixture.ServiceProvider.GetService<IUsuarioRepository>();
            _perfilRepository = fixture.ServiceProvider.GetService<IPerfilRepository>();
        }

        [Fact]
        public async Task Deve_Criar_Usuario_Com_Senha()
        {
            var (resultado, usuario) = await _fixture.GerarUsuarioRandomico(_usuarioRepository);
            resultado.Succeeded.Should().BeTrue();

            var resultadoEsperado = await _usuarioRepository.ObterPorNomeAsync(usuario.UserName);
            resultadoEsperado.Should().NotBeNull();
            resultadoEsperado.Should().BeEquivalentTo(usuario);
        }

        [Fact]
        public async Task Deve_Atualizar_Usuario()
        {
            var (resultadoCriar, usuario) = await _fixture.GerarUsuarioRandomico(_usuarioRepository);
            resultadoCriar.Succeeded.Should().BeTrue();

            usuario.Email = "update@update.com";
            usuario.PhoneNumber = "update";
            usuario.UserName = "bba33a9f-2392-4a3e-8c10-add549cb9bfe";

            var resultadoAtualizar = await _usuarioRepository.AtualizarAsync(usuario);
            resultadoAtualizar.Succeeded.Should().BeTrue();
        }


        [Fact]
        public async Task Deve_Deletar_Usuario()
        {
            var (resultadoCriar, usuario) = await _fixture.GerarUsuarioRandomico(_usuarioRepository);
            resultadoCriar.Succeeded.Should().BeTrue();

            var resultado = await _usuarioRepository.ExcluirAsync(usuario);
            resultado.Succeeded.Should().BeTrue();
        }

        [Fact]
        public async Task Deve_Obter_Usuario_Por_Id()
        {
            var (resultadoCriar, usuario) = await _fixture.GerarUsuarioRandomico(_usuarioRepository);
            resultadoCriar.Succeeded.Should().BeTrue();

            var usuarioAplicacao = await _usuarioRepository.ObterPorIdAsync(usuario.Id);
            usuarioAplicacao.Should().NotBeNull();
            usuarioAplicacao.Should().BeEquivalentTo(usuario);
        }

        [Fact]
        public async Task Deve_Obter_Usuario_Por_Nome()
        {
            var (resultadoCriar, usuario) = await _fixture.GerarUsuarioRandomico(_usuarioRepository);
            resultadoCriar.Succeeded.Should().BeTrue();

            var usuarioAplicacao = await _usuarioRepository.ObterPorNomeAsync(usuario.UserName);
            usuarioAplicacao.Should().NotBeNull();
            usuarioAplicacao.Should().BeEquivalentTo(usuario);
        }

        [Fact]
        public async Task Deve_Obter_Usuario_Por_Nome_Com_Include_Perfil()
        {
            var perfil = new PerfilFaker().Generate();
            var resultadoCadastroPerfil = await _perfilRepository.CriarAsync(perfil);
            resultadoCadastroPerfil.Succeeded.Should().BeTrue();

            var user = new UsuarioFaker().Generate();
            user.UsuariosPerfis = new List<UsuarioPerfil>
            {
                new UsuarioPerfil {RoleId = perfil.Id}
            };
            var resultadoCadastroUsuario = await _usuarioRepository.CriarAsync(user, "123456Abc%&");
            resultadoCadastroUsuario.Succeeded.Should().BeTrue();

            var resultadoEsperado = user;
            resultadoEsperado.UsuariosPerfis.First().Perfil = perfil;


            var usuarioResultado = await _usuarioRepository.ObterPorNomeAsync(user.UserName, new[] {nameof(Usuario.UsuariosPerfis), $"{nameof(Usuario.UsuariosPerfis)}.{nameof(UsuarioPerfil.Perfil)}"});
            usuarioResultado.Should().BeEquivalentTo(resultadoEsperado);
        }
        
        [Fact]
        public async Task Deve_Obter_Usuario_Por_Login()
        {
            var (resultadoCriar, usuario) = await _fixture.GerarUsuarioRandomico(_usuarioRepository);
            resultadoCriar.Succeeded.Should().BeTrue();
            
            var usuarioAplicacao = await _usuarioRepository.ObterPorLoginAsync(usuario.Login);
            usuarioAplicacao.Should().NotBeNull();
            usuarioAplicacao.Should().BeEquivalentTo(usuario); 
        }
        
        [Fact]
        public async Task Deve_Obter_Usuario_Por_Login_Com_Include_Perfil()
        {
            var perfil = new PerfilFaker().Generate();
            var resultadoCadastroPerfil = await _perfilRepository.CriarAsync(perfil);
            resultadoCadastroPerfil.Succeeded.Should().BeTrue();

            var user = new UsuarioFaker().Generate();
            user.UsuariosPerfis = new List<UsuarioPerfil>
            {
                new UsuarioPerfil {RoleId = perfil.Id}
            };
            var resultadoCadastroUsuario = await _usuarioRepository.CriarAsync(user, "123456Abc%&");
            resultadoCadastroUsuario.Succeeded.Should().BeTrue();

            var resultadoEsperado = user;
            resultadoEsperado.UsuariosPerfis.First().Perfil = perfil;


            var usuarioResultado = await _usuarioRepository.ObterPorLoginAsync(user.Login, new[] {nameof(Usuario.UsuariosPerfis), $"{nameof(Usuario.UsuariosPerfis)}.{nameof(UsuarioPerfil.Perfil)}"});
            usuarioResultado.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task Deve_Listar_Todos_Usuarios()
        {
            var (resultadoCriar, _) = await _fixture.GerarUsuarioRandomico(_usuarioRepository);
            resultadoCriar.Succeeded.Should().BeTrue();

            var usuarios = await _usuarioRepository.ListarPorAsync();
            usuarios.Should().NotBeNull();
        }

        [Fact]
        public async Task Deve_Listar_Todos_Usuarios_Com_Filtro()
        {
            var (resultadoCriar, usuario) = await _fixture.GerarUsuarioRandomico(_usuarioRepository);
            resultadoCriar.Succeeded.Should().BeTrue();

            var usuarios = (await _usuarioRepository.ListarPorAsync(lnq => lnq.NormalizedUserName == usuario.NormalizedUserName)).ToList();
            usuarios.Should().NotBeNull();
            usuarios.Single().Should().BeEquivalentTo(usuario);
        }
        
        [Fact]
        public async Task Deve_Listar_Paginado()
        {
            var quantidadeItems = 10;
            var usuarios = new List<Usuario>();
            for (int i = 0; i < quantidadeItems; i++)
            {
                var (_, usuario) = await _fixture.GerarUsuarioRandomico(_usuarioRepository);
                usuarios.Add(usuario);
            }

            var pagedParams = new PagedParam(5, 1);
            var idUsuarios = usuarios.Select(lnq => lnq.Id);
            Expression<Func<Usuario, bool>> filtro = lnq => idUsuarios.Contains(lnq.Id);

            var resultadoPrimeiraPagina = await _usuarioRepository.ListarPorAsync(filtro, default, pagedParams);
            resultadoPrimeiraPagina.Count.Should().Be(5);
            resultadoPrimeiraPagina.PageInfo.HasNext.Should().BeTrue();
            resultadoPrimeiraPagina.PageInfo.HasPrevious.Should().BeFalse();
            resultadoPrimeiraPagina.PageInfo.PageNumber.Should().Be(1);
            resultadoPrimeiraPagina.PageInfo.PageSize.Should().Be(5);
            resultadoPrimeiraPagina.PageInfo.TotalPages.Should().Be(2);
            
            
            var resultadoSegundaPagina = await _usuarioRepository.ListarPorAsync(filtro, default, pagedParams.Next());
            resultadoSegundaPagina.Count.Should().Be(5);
            resultadoSegundaPagina.PageInfo.HasNext.Should().BeFalse();
            resultadoSegundaPagina.PageInfo.HasPrevious.Should().BeTrue();
            resultadoSegundaPagina.PageInfo.PageNumber.Should().Be(2);
            resultadoSegundaPagina.PageInfo.PageSize.Should().Be(5);
            resultadoSegundaPagina.PageInfo.TotalPages.Should().Be(2);
        }

        [Fact]
        public async Task Deve_Atualizar_Senha_Usuario()
        {
            var (_, usuario) = await _fixture.GerarUsuarioRandomico(_usuarioRepository);
            var usuarioAplicacao = await _usuarioRepository.ObterPorIdAsync(usuario.Id);
            var senhaAntigaUsuario = usuario.PasswordHash;
            await _usuarioRepository.AtualizarSenha(usuarioAplicacao, "123aBCD%@");
            usuarioAplicacao.PasswordHash.Should().NotBeSameAs(senhaAntigaUsuario);
        }
    }
}
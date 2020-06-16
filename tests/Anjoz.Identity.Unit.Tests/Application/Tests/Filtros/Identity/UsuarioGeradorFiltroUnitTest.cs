using System.Linq;
using Anjoz.Identity.Application.Dtos.Identity.Usuario;
using Anjoz.Identity.Application.Filtros.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Infrastructure.Interfaces.Filter;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using FluentAssertions;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Application.Tests.Filtros.Identity
{
    public class UsuarioGeradorFiltroUnitTest
    {
        private readonly IGeradorFiltro<Usuario, UsuarioFiltroDto> _geradorFiltro;

        public UsuarioGeradorFiltroUnitTest()
        {
            _geradorFiltro = new UsuarioGeradorFiltro();
        }

        [Fact]
        public void Deve_Filtrar_Usuarios_Por_Id()
        {
            var filtro = new UsuarioFiltroDto {Id = 1};

            var where = _geradorFiltro.Gerar(filtro);

            var usuarios = UsuarioUtils.Usuarios.Where(where.Compile());

            usuarios.Should().NotBeNull().And.HaveCount(1);
        }

        [Fact]
        public void Deve_Filtrar_Usuarios_Por_Nome()
        {
            var filtro = new UsuarioFiltroDto {Nome = "teste"};

            var where = _geradorFiltro.Gerar(filtro);

            var usuarios = UsuarioUtils.Usuarios.Where(where.Compile());

            usuarios.Should().NotBeNull().And.HaveCount(10);
        }

        [Fact]
        public void Deve_Filtrar_Usuarios_Por_Login()
        {
            var filtro = new UsuarioFiltroDto {Login = "teste1"};
            
            var where = _geradorFiltro.Gerar(filtro);

            var usuarios = UsuarioUtils.Usuarios.Where(where.Compile());

            usuarios.Should().NotBeNull().And.HaveCount(2);
        }

        [Fact]
        public void Deve_Filtrar_Usuarios_Por_Email()
        {
            var filtro = new UsuarioFiltroDto {Email = "teste1"};

            var where = _geradorFiltro.Gerar(filtro);

            var usuarios = UsuarioUtils.Usuarios.Where(where.Compile());

            usuarios.Should().NotBeNull().And.HaveCount(2);   
        }
        
        [Fact]
        public void Deve_Filtrar_Usuarios_Por_Telefone()
        {
            var filtro = new UsuarioFiltroDto {Telefone = "1"};

            var where = _geradorFiltro.Gerar(filtro);

            var usuarios = UsuarioUtils.Usuarios.Where(where.Compile());

            usuarios.Should().NotBeNull().And.HaveCount(2);   
        }

        [Fact]
        public void Deve_Filtrar_Por_Codigo_Equipe()
        {
            var filtro = new UsuarioFiltroDto {CodigoEquipe = 1};
            var where = _geradorFiltro.Gerar(filtro);
            var usuarios = UsuarioUtils.Usuarios.Where(where.Compile());
            usuarios.Should().NotBeNull().And.HaveCount(3);
        }
    }
}
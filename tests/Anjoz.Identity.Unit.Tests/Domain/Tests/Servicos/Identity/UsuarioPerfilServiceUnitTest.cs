using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Servicos.Identity;
using Anjoz.Identity.Unit.Tests.Domain.Fixtures.Identity;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using FluentAssertions;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Domain.Tests.Servicos.Identity
{
    public class UsuarioPerfilServiceUnitTest : IClassFixture<UsuarioPerfilServiceFixture>
    {
        private readonly UsuarioPerfilService _usuarioPerfilService;

        public UsuarioPerfilServiceUnitTest(UsuarioPerfilServiceFixture fixture)
        {
            _usuarioPerfilService = new UsuarioPerfilService(fixture.InicializarUsuarioPerfilRepository(),
                fixture.InicializarPerfilSerivce(), fixture.DomainValidator<UsuarioPerfil>());
        }

        [Fact]
        public void Deve_Popular_Vinculo_Usuario_Perfil()
        {
            var resultadoEsperado = new List<UsuarioPerfil>
            {
                new UsuarioPerfil {UserId = 1, RoleId = 1},
                new UsuarioPerfil {UserId = 1, RoleId = 2},
                new UsuarioPerfil {UserId = 1, RoleId = 3},
                new UsuarioPerfil {UserId = 1, RoleId = 4},
                new UsuarioPerfil {UserId = 1, RoleId = 5}
            };

            var resultado = _usuarioPerfilService.PopularVinculos(1, new[] {1, 2, 3, 4, 5});

            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task Deve_Listar_Todos_Vinculos_Usuario_Perfil()
        {
            var resultadoEsperado = UsuarioPerfilUtils.UsuariosPerfis.Where(lnq => lnq.UserId == 1);
            var resultado = await _usuarioPerfilService.ListarTodosVinculosEntidade(1);

            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task Deve_Listar_Todos_Perfis()
        {
            var resultadoEsperado = PerfilUtils.Perfis.Where(lnq => new[] {1, 2, 3, 4, 5}.Contains(lnq.Id)).Select(lnq => lnq.Id);
            var resultado = await _usuarioPerfilService.ListarTodosVinculos(new[] {1, 2, 3, 4, 5});

            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }
    }
}
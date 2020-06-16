using System.Collections.Generic;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Entidades.Login;
using FluentAssertions;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Domain.Tests.Entidades.Login
{
    public class LoginUsuarioUnitTest
    {
        private readonly Usuario _usuario = new Usuario {Id = 1, UserName = "Teste"};
        private readonly List<Claim> _claims = new List<Claim> {new Claim {Id = 1, Valor = "Valor1"}, new Claim {Id = 1, Valor = "Valor2"}};

        [Fact]
        public void Deve_Inicializar_Login_Usuario()
        {
            var resultadoEsperado = new LoginUsuario {Usuario = _usuario, Claims = _claims};
            var loginUsuario = LoginUsuario.Inicializar(_usuario, _claims);
            
            loginUsuario.Should().BeEquivalentTo(resultadoEsperado);
        }
    }
}
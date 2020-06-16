using System.Collections.Generic;
using System.Threading.Tasks;
using Anjoz.Identity.Application.Contratos.Login;
using Anjoz.Identity.Application.Dtos.Login;
using Anjoz.Identity.Application.Servicos;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Unit.Tests.Application.Fixture;
using Anjoz.Identity.Utils.Tests.Utils;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using FluentAssertions;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Application.Tests.Servicos
{
    public class LoginApplicationServiceUnitTest : IClassFixture<LoginApplicationServiceUnitTestFixture>
    {
        private readonly LoginApplicationServiceUnitTestFixture _fixture;
        private readonly ILoginApplicationService _loginApplicationService;

        public LoginApplicationServiceUnitTest(LoginApplicationServiceUnitTestFixture fixture)
        {
            _fixture = fixture;
            _loginApplicationService = new LoginApplicationService(fixture.Mapper, fixture.InicializarLoginService());
        }

        [Fact]
        public async Task Deve_Realizar_Login()
        {
            var loginDto = new LoginDto {Login = "teste1", Senha = "TESTE1"};
            var loginUsuarioDto = await _loginApplicationService.Login(loginDto);

            var resultadoEsperado = new LoginUsuarioDto
            {
                Nome = "teste1",
                Login = "teste1",
                Email = "teste1@teste.com",
                AcessToken = LoginUtils.Token,
                Claims = _fixture.Mapper.Map<IEnumerable<Claim>, IEnumerable<LoginClaimDto>>(ClaimUtils.Claims),
            };

            loginUsuarioDto.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task Deve_Retornar_Nullo_Quando_Usuario_Ou_senha_Invalido()
        {
            var login = new LoginDto {Login = "teste", Senha = "TESTE1"};
            var loginUsuarioDto = await _loginApplicationService.Login(login);

            loginUsuarioDto.Should().BeNull();
        }
    }
}
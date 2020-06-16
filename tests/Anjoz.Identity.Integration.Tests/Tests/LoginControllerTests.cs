using System.Threading.Tasks;
using Anjoz.Identity.Application.Contratos.Login;
using Anjoz.Identity.Application.Dtos.Login;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Integration.Tests.Extensions;
using Anjoz.Identity.Integration.Tests.Fixtures.Base;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Anjoz.Identity.Integration.Tests.Tests
{
    public class LoginControllerTests : BaseIntegrationTestFixture
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILoginApplicationService _loginApplicationService;

        public LoginControllerTests()
        {
            _usuarioService = ServiceProvider.GetService<IUsuarioService>();
            _loginApplicationService = ServiceProvider.GetService<ILoginApplicationService>();
        }
        
        [Fact]
        public async Task Deve_Realizar_Login()
        {
            var usuario = new Usuario {UserName = "TesteLogin", Login = "TesteLogin", Email = "TesteLogin@teste.com", PasswordHash = "123Ab%%"};
            await _usuarioService.CriarAsync(usuario);

            var loginDto = new LoginDto {Login = "TesteLogin", Senha = "123Ab%%"};

            var loginUsuarioDto = await BaseWebApiExtensions.EnviarAsync<LoginUsuarioDto, LoginDto>($"{BaseUrl}/login", loginDto, Client.PostAsync, StatusCodes.Status200OK);

            var resultadoEsperado = await _loginApplicationService.Login(loginDto);
            
            loginUsuarioDto.Should().BeEquivalentTo(resultadoEsperado, opt => 
                opt.Excluding(prop => prop.AcessToken));

            loginUsuarioDto.AcessToken.Should().NotBeNullOrEmpty();
        }
    }
}
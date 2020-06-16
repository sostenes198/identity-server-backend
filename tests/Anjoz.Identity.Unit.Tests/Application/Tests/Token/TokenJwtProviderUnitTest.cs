using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Anjoz.Identity.Application.Token.Jwt;
using Anjoz.Identity.Domain.Contratos.Token;
using Anjoz.Identity.Domain.Entidades.Login;
using Anjoz.Identity.Infrastructure.Configuracoes.Jwt;
using Anjoz.Identity.Unit.Tests.Application.Fixture.Token;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;
using Claim = System.Security.Claims.Claim;

namespace Anjoz.Identity.Unit.Tests.Application.Tests.Token
{
    public class TokenJwtProviderUnitTest : IClassFixture<TokenProviderUnitTestFixture>
    {
        private readonly ITokenProvider<LoginUsuario> _tokenProvider;

        public TokenJwtProviderUnitTest(TokenProviderUnitTestFixture unitTestFixture)
        {
            _tokenProvider = new UsuarioTokenJwtProvider(unitTestFixture.ServiceProvider.GetService<IOptions<JwtConfiguration>>());
        }

        [Fact]
        public async Task Deve_Gerar_Token_Valido()
        {
            var payloadClaims = new[] {"nameid", "unique_name", "email", "Permissoes"};
            var payloadEsperado = new[] {"nameid", "unique_name", "email", "Permissoes", "nbf", "exp", "iat"};
            IEnumerable<Claim> claimsEsperadas = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "teste1"),
                new Claim(ClaimTypes.Email, "teste1@teste.com"),
                new Claim("Claims", "VALUE1"),
                new Claim("Claims", "VALUE2"),
                new Claim("Claims", "VALUE3"),
                new Claim("Claims", "VALUE4"),
                new Claim("Claims", "VALUE5"),
            };
            var usuario = LoginUsuarioUtils.LoginUsuario;
            usuario.Claims = new List<Identity.Domain.Entidades.Identity.Claim>
            {
                new Identity.Domain.Entidades.Identity.Claim {Id = 1, Valor = "value1", ValorNormalizado = "VALUE1"},
                new Identity.Domain.Entidades.Identity.Claim {Id = 1, Valor = "value2", ValorNormalizado = "VALUE2"},
                new Identity.Domain.Entidades.Identity.Claim {Id = 1, Valor = "value3", ValorNormalizado = "VALUE3"},
                new Identity.Domain.Entidades.Identity.Claim {Id = 1, Valor = "value4", ValorNormalizado = "VALUE4"},
                new Identity.Domain.Entidades.Identity.Claim {Id = 1, Valor = "value5", ValorNormalizado = "VALUE5"},
            };

            var token = await _tokenProvider.GerarToken(usuario);

            var handler = new JwtSecurityTokenHandler();
            var tokenDecode = handler.ReadToken(token) as JwtSecurityToken ?? new JwtSecurityToken();

            tokenDecode.Should().NotBeNull();

            tokenDecode.Claims.Select(lnq => lnq.Type).Distinct().Should().BeEquivalentTo(payloadEsperado);

            var claims = tokenDecode.Claims.Where(lnq => payloadClaims.Contains(lnq.Type));
            claims.Select(lnq => lnq.Value).Should().BeEquivalentTo(claimsEsperadas.Select(lnq => lnq.Value));
        }
    }
}
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Entidades.Login;
using Anjoz.Identity.Infrastructure.Configuracoes.Jwt;
using Anjoz.Package.Authentication.Domain.Principal;
using Microsoft.Extensions.Options;
using Claim = System.Security.Claims.Claim;
using ClaimIdentity = Anjoz.Identity.Domain.Entidades.Identity.Claim;

namespace Anjoz.Identity.Application.Token.Jwt
{
    public class UsuarioTokenJwtProvider : TokenJwtProvider<LoginUsuario>
    {
        private LoginUsuario _loginUsuario;

        public UsuarioTokenJwtProvider(IOptions<JwtConfiguration> optionsJwtConfiguration)
            : base(optionsJwtConfiguration)
        {
        }

        public override Task<string> GerarToken(LoginUsuario param)
        {
            _loginUsuario = param;
            var claims = GerarClaims();
            return Task.FromResult(ObterToken(claims));
        }

        private ICollection<Claim> GerarClaims()
        {
            var claims = ClaimsPrincipaisUsuario;

            foreach (var claim in _loginUsuario.Claims)
                claims.Add(new Claim(AnjozClaimTypes.ClaimTypePermission, claim.ValorNormalizado));

            return claims;
        }

        private ICollection<Claim> ClaimsPrincipaisUsuario => new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, _loginUsuario.Usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, _loginUsuario.Usuario.UserName),
            new Claim(ClaimTypes.Email, _loginUsuario.Usuario.Email)
        };
    }
}
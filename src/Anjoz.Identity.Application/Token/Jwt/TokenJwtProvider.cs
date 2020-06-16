using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Token;
using Anjoz.Identity.Infrastructure.Configuracoes.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Anjoz.Identity.Application.Token.Jwt
{
    public abstract class TokenJwtProvider<TParams> : ITokenProvider<TParams>
        where TParams : class
    {
        protected readonly JwtConfiguration JwtConfiguration;

        public TokenJwtProvider(IOptions<JwtConfiguration> optionsJwtConfiguration)
        {
            JwtConfiguration = optionsJwtConfiguration.Value;
        }
        public abstract Task<string> GerarToken(TParams param);

        protected string ObterToken(IEnumerable<Claim> claims)
        {
            var chave = GerarChave();
            var credenciais = GerarCredenciais(chave, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = GerarTokenDescriptor(claims, credenciais);

            return EscreverToken(tokenDescriptor);
        }

        protected virtual SymmetricSecurityKey GerarChave()
        {
            var secretKeyBytes = Encoding.ASCII.GetBytes(JwtConfiguration.Chave);
            return new SymmetricSecurityKey(secretKeyBytes);
        }
        
        protected virtual SigningCredentials GerarCredenciais(SymmetricSecurityKey chaveSimetrica, string algoritmo)
        {
            return new SigningCredentials(chaveSimetrica, algoritmo);
        }

        protected virtual SecurityTokenDescriptor GerarTokenDescriptor(IEnumerable<Claim> claims, SigningCredentials credenciais)
        {
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims ?? new List<Claim>()),
                SigningCredentials = credenciais,
                Expires = DateTime.Now.Date.AddDays(JwtConfiguration.Duracao),
                NotBefore = DateTime.Now
            };
        }
        
        protected virtual string EscreverToken(SecurityTokenDescriptor tokenDescriptor)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Entidades.Login;

namespace Anjoz.Identity.Utils.Tests.Utils.Identity
{
    public sealed class LoginUsuarioUtils
    {
        public const string NomeUsuarioTeste = "teste1";
        public static LoginUsuario LoginUsuario => new LoginUsuario
        {
            Usuario = new Usuario
            {
                Id = 1,
                Email = "teste1@teste.com",
                NormalizedEmail = "TESTE1@TESTE.COM",
                Login = NomeUsuarioTeste, 
                LoginNormalizado = NomeUsuarioTeste.ToUpper(),
                UserName = NomeUsuarioTeste,
                NormalizedUserName = NomeUsuarioTeste.ToUpper(),
                PhoneNumber = "111111",
                CodigoEquipe = 1
            },
            Claims = ClaimUtils.Claims,
            AcessToken = LoginUtils.Token
        };
    }
}
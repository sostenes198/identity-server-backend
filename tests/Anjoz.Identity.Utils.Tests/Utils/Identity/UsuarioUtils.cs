using System.Collections.Generic;
using Anjoz.Identity.Domain.Entidades.Identity;
using FluentAssertions.Equivalency;

namespace Anjoz.Identity.Utils.Tests.Utils.Identity
{
    public sealed class UsuarioUtils
    {
        public const string NomeUsuarioTeste = "teste1";

        public static ICollection<Usuario> Usuarios => new List<Usuario>
        {
            new Usuario {Id = 1, CodigoEquipe = 1, Email = "teste1@teste.com", NormalizedEmail = "TESTE1@TESTE.COM", Login = NomeUsuarioTeste, LoginNormalizado = NomeUsuarioTeste.ToUpper(), UserName = NomeUsuarioTeste, NormalizedUserName = NomeUsuarioTeste.ToUpper(), PhoneNumber = "111111"},
            new Usuario {Id = 2, CodigoEquipe = 1, Email = "teste2@teste.com", NormalizedEmail = "TESTE2@TESTE.COM", Login = "teste2", LoginNormalizado = "TESTE2", UserName = "teste2", NormalizedUserName = "TESTE2", PhoneNumber = "222222"},
            new Usuario {Id = 3, CodigoEquipe = 1, Email = "teste3@teste.com", NormalizedEmail = "TESTE3@TESTE.COM", Login = "teste3", LoginNormalizado = "TESTE3", UserName = "teste3", NormalizedUserName = "TESTE3", PhoneNumber = "333333"},
            new Usuario {Id = 4, CodigoEquipe = 2,  Email = "teste4@teste.com", NormalizedEmail = "TESTE4@TESTE.COM", Login = "teste4", LoginNormalizado = "TESTE4", UserName = "teste4", NormalizedUserName = "TESTE4", PhoneNumber = "444444"},
            new Usuario {Id = 5, CodigoEquipe = 2,  Email = "teste5@teste.com", NormalizedEmail = "TESTE5@TESTE.COM", Login = "teste5", LoginNormalizado = "TESTE5", UserName = "teste5", NormalizedUserName = "TESTE5", PhoneNumber = "555555"},
            new Usuario {Id = 6, CodigoEquipe = 2,  Email = "teste6@teste.com", NormalizedEmail = "TESTE6@TESTE.COM", Login = "teste6", LoginNormalizado = "TESTE6", UserName = "teste6", NormalizedUserName = "TESTE6", PhoneNumber = "666666"},
            new Usuario {Id = 7, CodigoEquipe = 3,  Email = "teste7@teste.com", NormalizedEmail = "TESTE7@TESTE.COM", Login = "teste7", LoginNormalizado = "TESTE7", UserName = "teste7", NormalizedUserName = "TESTE7", PhoneNumber = "777777"},
            new Usuario {Id = 8, CodigoEquipe = 3,  Email = "teste8@teste.com", NormalizedEmail = "TESTE8@TESTE.COM", Login = "teste8", LoginNormalizado = "TESTE8", UserName = "teste8", NormalizedUserName = "TESTE8", PhoneNumber = "888888"},
            new Usuario {Id = 9, Email = "teste9@teste.com", NormalizedEmail = "TESTE9@TESTE.COM", Login = "teste9", LoginNormalizado = "TESTE9", UserName = "teste9", NormalizedUserName = "TESTE9", PhoneNumber = "999999"},
            new Usuario {Id = 10, Email = "teste10@teste.com", NormalizedEmail = "TESTE10@TESTE.COM", Login = "teste10", LoginNormalizado = "TESTE10",  UserName = "teste10", NormalizedUserName = "TESTE10", PhoneNumber = "101010"},
        };

        public static EquivalencyAssertionOptions<Usuario> PropriedadesParaIgnorar(EquivalencyAssertionOptions<Usuario> opt) =>
            opt.Excluding(prop => prop.ConcurrencyStamp);
    }
}
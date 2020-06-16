using System.Collections.Generic;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Integration.Tests.Fixtures.Base;

namespace Anjoz.Identity.Integration.Tests.Fixtures.Identity
{
    public class UsuariosControllerFixture : BaseIntegrationTestFixture
    {
        public readonly ICollection<Usuario> UsuariosParaListar = new List<Usuario>
        {
            new Usuario {UserName = "UsuarioTeste1", Login = "UsuarioTeste1", PasswordHash = "Abc321@@", Email = "teste1@teste.com"},
            new Usuario {UserName = "UsuarioTeste2", Login = "UsuarioTeste2", PasswordHash = "Abc321@@", Email = "teste2@teste.com"},
            new Usuario {UserName = "UsuarioTeste3", Login = "UsuarioTeste3", PasswordHash = "Abc321@@", Email = "teste3@teste.com"},
            new Usuario {UserName = "UsuarioTeste4", Login = "UsuarioTeste4", PasswordHash = "Abc321@@", Email = "teste4@teste.com"},
        };

        public readonly ICollection<Usuario> UsuariosParaListarComFiltro = new List<Usuario>
        {
            new Usuario {UserName = "UsuarioAATeste1", Login = "UsuarioAATeste1", PasswordHash = "Abc321@@", Email = "teste1@teste.com"},
            new Usuario {UserName = "UsuarioAATeste2", Login = "UsuarioAATeste1", PasswordHash = "Abc321@@", Email = "teste2@teste.com"},
            new Usuario {UserName = "UsuarioAATeste3", Login = "UsuarioAATeste1", PasswordHash = "Abc321@@", Email = "teste3@teste.com"},
            new Usuario {UserName = "UsuarioAATeste4", Login = "UsuarioAATeste1", PasswordHash = "Abc321@@", Email = "teste4@teste.com"},
        };

        public readonly ICollection<Claim> Claims = new List<Claim>
        {
            new Claim {Valor = "UsuarioBBValor1", Descricao = "UsuarioBBValor1"},
            new Claim {Valor = "UsuarioBBValor2", Descricao = "UsuarioBBValor2"},
            new Claim {Valor = "UsuarioBBValor3", Descricao = "UsuarioBBValor3"},
            new Claim {Valor = "UsuarioBBValor4", Descricao = "UsuarioBBValor4"},
        };
        
        public readonly ICollection<Claim> ClaimsUsuario = new List<Claim>
        {
            new Claim {Valor = "UsuarioClaimBBValor1", Descricao = "UsuarioClaimBBValor1"},
            new Claim {Valor = "UsuarioClaimBBValor2", Descricao = "UsuarioClaimBBValor2"},
            new Claim {Valor = "UsuarioClaimBBValor3", Descricao = "UsuarioClaimBBValor3"},
            new Claim {Valor = "UsuarioClaimBBValor4", Descricao = "UsuarioClaimBBValor4"},
        };
        
        public readonly ICollection<Claim> ClaimsAtualizar = new List<Claim>
        {
            new Claim {Valor = "UsuarioAABBValor1", Descricao = "UsuarioBBValor1"},
            new Claim {Valor = "UsuarioAABBValor2", Descricao = "UsuarioBBValor2"},
        };

        public readonly ICollection<Perfil> Perfis = new List<Perfil>()
        {
            new Perfil {Name = "UsuarioBBValor1"},
            new Perfil {Name = "UsuarioBBValor2"},
            new Perfil {Name = "UsuarioBBValor3"},
            new Perfil {Name = "UsuarioBBValor4"},
            new Perfil {Name = "UsuarioBBValor5"},
        };
        
        public readonly ICollection<Perfil> PerfisAtualizar = new List<Perfil>()
        {
            new Perfil {Name = "UsuarioAABBValor1"},
            new Perfil {Name = "UsuarioAABBValor2"},
        };
    }
}
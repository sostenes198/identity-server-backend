using Anjoz.Identity.Domain.Contratos.Validadores;
using Anjoz.Identity.Domain.Validadores.Identity.Usuario;
using Anjoz.Identity.Utils.Tests.Fakers.Identity;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Domain.Tests.Validadores.Identity.Usuario
{
    public class UsuarioSaveServiceValidatorUnitTest : ValidadorBaseUnitTest
    {
        private readonly ISaveServiceValidator<Anjoz.Identity.Domain.Entidades.Identity.Usuario> _saveServiceValidator;

        public UsuarioSaveServiceValidatorUnitTest()
        {
            _saveServiceValidator = new UsuarioSaveServiceValidator();
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Nome_Vazio()
        {
            var usuario = new UsuarioFaker().Generate();
            usuario.Id = 1;
            usuario.PasswordHash = "123Ab@@";
            usuario.UserName = default;
            
            InvocarValidacao(_saveServiceValidator, usuario);
        }
        

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Nome_Maior_Que_256_Caracteres()
        {
            var usuario = new UsuarioFaker().Generate();
            usuario.Id = 1;
            usuario.PasswordHash = "123Ab@@";
            usuario.UserName = new string('*', 257);
            
            InvocarValidacao(_saveServiceValidator, usuario);
        }
        
        [Fact]
        public void Deve_Lancar_Excecao_Quando_Login_Vazio()
        {
            var usuario = new UsuarioFaker().Generate();
            usuario.Id = 1;
            usuario.PasswordHash = "123Ab@@";
            usuario.Login = default;
            
            InvocarValidacao(_saveServiceValidator, usuario);
        }
        
        [Fact]
        public void Deve_Lancar_Excecao_Quando_Login_Maior_Que_256_Caracteres()
        {
            var usuario = new UsuarioFaker().Generate();
            usuario.Id = 1;
            usuario.PasswordHash = "123Ab@@";
            usuario.Login = new string('*', 257);
            
            InvocarValidacao(_saveServiceValidator, usuario);
        }
        
        [Fact]
        public void Deve_Lancar_Excecao_Quando_Senha_Vazia()
        {
            var usuario = new UsuarioFaker().Generate();
            usuario.Id = 1;
            usuario.PasswordHash = default;
            
            InvocarValidacao(_saveServiceValidator, usuario);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Senha_Maior_Que_256_Caracteres()
        {
            var usuario = new UsuarioFaker().Generate();
            usuario.Id = 1;
            usuario.PasswordHash = new string('*', 257);

            InvocarValidacao(_saveServiceValidator, usuario);
        }
        
        [Fact]
        public void Deve_Lancar_Excecao_Quando_Email_Vazio()
        {
            var usuario = new UsuarioFaker().Generate();
            usuario.Id = 1;
            usuario.PasswordHash = "abc@@ABC";
            usuario.Email = default;
            
            InvocarValidacao(_saveServiceValidator, usuario);
        }
        
          
        [Fact]
        public void Deve_Lancar_Excecao_Quando_Email_Invalido()
        {
            var usuario = new UsuarioFaker().Generate();
            usuario.Id = 1;
            usuario.PasswordHash = "abc@@ABC";
            usuario.Email = "EmailNaoValido";
            
            InvocarValidacao(_saveServiceValidator, usuario);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Email_Maior_Que_256_Caracteres()
        {
            var usuario = new UsuarioFaker().Generate();
            usuario.Id = 1;
            usuario.PasswordHash = "abc@@ABC";
            usuario.Email = $"{new string('*', 257)}@teste.com";

            InvocarValidacao(_saveServiceValidator, usuario);
        }
        
        [Fact]
        public void Deve_Lancar_Excecao_Quando_Telefone_Maior_Que_256_Caracteres()
        {
            var usuario = new UsuarioFaker().Generate();
            usuario.Id = 1;
            usuario.PasswordHash = "abc@@ABC";
            usuario.PhoneNumber = new string('*', 257);

            InvocarValidacao(_saveServiceValidator, usuario);
        }

       
    }
}
using Anjoz.Identity.Domain.Contratos.Validadores;
using Anjoz.Identity.Domain.Validadores.Identity.Usuario;
using Anjoz.Identity.Domain.VO;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Domain.Tests.Validadores.Identity.Usuario
{
    public class UsuarioAlteracaoSenhaVoCustomValidatorUnitTest : ValidadorBaseUnitTest
    {
        private readonly ICustomServiceValidator<UsuarioAlteracaoSenhaVo> _customServiceValidator;

        public UsuarioAlteracaoSenhaVoCustomValidatorUnitTest()
        {
            _customServiceValidator = new UsuarioAlteracaoSenhaVoCustomValidator();
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Id_Default()
        {
            var usuarioAlteracaoSenha = new UsuarioAlteracaoSenhaVo
            {
                Id = 0,
                ConfirmacaoSenha = "123",
                NovaSenha = "123",
                SenhaAtual = "155"
            };
            
            InvocarValidacao(_customServiceValidator, usuarioAlteracaoSenha);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Senha_Atual_Nao_Preenchido()
        {
            var usuarioAlteracaoSenha = new UsuarioAlteracaoSenhaVo
            {
                Id = 1,
                ConfirmacaoSenha = "123",
                NovaSenha = "123",
                SenhaAtual = ""
            };
            
            InvocarValidacao(_customServiceValidator, usuarioAlteracaoSenha);
        }
        
        [Fact]
        public void Deve_Lancar_Excecao_Quando_Nova_Senha_Nao_Preenchido()
        {
            var usuarioAlteracaoSenha = new UsuarioAlteracaoSenhaVo
            {
                Id = 1,
                ConfirmacaoSenha = "123",
                NovaSenha = "",
                SenhaAtual = "123"
            };
            
            InvocarValidacao(_customServiceValidator, usuarioAlteracaoSenha);
        }
        
        [Fact]
        public void Deve_Lancar_Excecao_Quando_Confirmacao_Senha_Nao_Preenchido()
        {
            var usuarioAlteracaoSenha = new UsuarioAlteracaoSenhaVo
            {
                Id = 1,
                ConfirmacaoSenha = "",
                NovaSenha = "123",
                SenhaAtual = "123"
            };
            
            InvocarValidacao(_customServiceValidator, usuarioAlteracaoSenha, 2);
        }
        
         [Fact]
        public void Deve_Lancar_Excecao_Quando_Nova_Senha_Diferente_De_Confirmacao_De_Senha()
        {
            var usuarioAlteracaoSenha = new UsuarioAlteracaoSenhaVo
            {
                Id = 1,
                ConfirmacaoSenha = "456",
                NovaSenha = "123",
                SenhaAtual = "123"
            };
            
            InvocarValidacao(_customServiceValidator, usuarioAlteracaoSenha);
        }
    }
}
using System.ComponentModel.DataAnnotations;
using Anjoz.Identity.Application.Dtos.Base;
using Anjoz.Identity.Domain.Configuracoes;

namespace Anjoz.Identity.Application.Dtos.Identity.Usuario.AlteracaoSenha
{
    public class UsuarioAlteracaoSenhaDto : BaseDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(ConfiguracaoAplicacao.TamanhoMaximoCamposString)]
        public string SenhaAtual { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(ConfiguracaoAplicacao.TamanhoMaximoCamposString)]
        public string NovaSenha { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(ConfiguracaoAplicacao.TamanhoMaximoCamposString)]
        [Compare(nameof(NovaSenha))]
        public string ConfirmacaoSenha { get; set; }
    }
}
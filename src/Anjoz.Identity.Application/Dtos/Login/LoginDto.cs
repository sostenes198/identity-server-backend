using System.ComponentModel.DataAnnotations;
using Anjoz.Identity.Domain.Configuracoes;

namespace Anjoz.Identity.Application.Dtos.Login
{
    public class LoginDto
    {
        [Required]
        [StringLength(ConfiguracaoAplicacao.TamanhoMaximoCamposString)]
        public string Login { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(ConfiguracaoAplicacao.TamanhoMaximoCamposString)]
        public string Senha { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Anjoz.Identity.Application.Dtos.Base;
using Anjoz.Identity.Domain.Configuracoes;

namespace Anjoz.Identity.Application.Dtos.Identity.Usuario
{
    public class UsuarioCriarDto : CriarEntidadeDto
    {
        public UsuarioCriarDto()
        {
            ClaimsId = new HashSet<int>();
            PerfisId = new HashSet<int>();
        }
        
        [Required]
        [StringLength(ConfiguracaoAplicacao.TamanhoMaximoCamposString)]
        public string Nome { get; set; }
        
        [Required]
        [StringLength(ConfiguracaoAplicacao.TamanhoMaximoCamposString)]
        public string Login { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(ConfiguracaoAplicacao.TamanhoMaximoCamposString)]
        public string Senha { get; set; }
        
        [Required]
        [StringLength(ConfiguracaoAplicacao.TamanhoMaximoCamposString)]
        [EmailAddress]
        public string Email { get; set; }
        
        [StringLength(ConfiguracaoAplicacao.TamanhoMaximoCamposString)]
        public string Telefone { get; set; }
        
        public int? CodigoEquipe { get; set; }
        
        public HashSet<int> ClaimsId { get; set; }
        
        public HashSet<int> PerfisId { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Anjoz.Identity.Application.Dtos.Base;
using Anjoz.Identity.Domain.Configuracoes;

namespace Anjoz.Identity.Application.Dtos.Identity.Usuario
{
    public class UsuarioAtualizarDto : AtualizarEntidadeDto
    {
        public UsuarioAtualizarDto()
        {
            ClaimsId = new HashSet<int>();
            PerfisId = new HashSet<int>();
        }
        
        [Required]
        public int Id { get; set; }
        
        [Required]
        [StringLength(ConfiguracaoAplicacao.TamanhoMaximoCamposString)]
        public string Nome { get; set; }
        
        [Required]
        [StringLength(ConfiguracaoAplicacao.TamanhoMaximoCamposString)]
        public string Login { get; set; }
        
        [Required]
        [EmailAddress]
        [StringLength(ConfiguracaoAplicacao.TamanhoMaximoCamposString)]
        public string Email { get; set; }
        
        
        [StringLength(ConfiguracaoAplicacao.TamanhoMaximoCamposString)]
        public string Telefone { get; set; }
        
        public int? CodigoEquipe { get; set; }
        
        public HashSet<int> ClaimsId { get; set; }
        
        public HashSet<int> PerfisId { get; set; }
    }
}
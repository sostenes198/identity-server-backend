using System.ComponentModel.DataAnnotations;
using Anjoz.Identity.Application.Dtos.Base;
using Anjoz.Identity.Domain.Configuracoes;

namespace Anjoz.Identity.Application.Dtos.Identity.Claim
{
    public class ClaimAtualizarDto : AtualizarEntidadeDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(ConfiguracaoAplicacao.TamanhoMaximoCamposString)]
        public string Valor { get; set; }
        
        [Required]
        [StringLength(ConfiguracaoAplicacao.TamanhoMaximoCamposString)]
        public string Descricao { get; set; }
    }
}
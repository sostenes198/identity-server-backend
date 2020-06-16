using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Anjoz.Identity.Application.Dtos.Base;
using Anjoz.Identity.Domain.Configuracoes;

namespace Anjoz.Identity.Application.Dtos.Identity.Perfil
{
    public class PerfilCriarDto : CriarEntidadeDto
    {
        public PerfilCriarDto()
        {
            ClaimsId = new HashSet<int>();
        }

        [Required]
        [StringLength(ConfiguracaoAplicacao.TamanhoMaximoCamposString)]
        public string Nome { get; set; }

        public HashSet<int> ClaimsId { get; set; }
    }
}
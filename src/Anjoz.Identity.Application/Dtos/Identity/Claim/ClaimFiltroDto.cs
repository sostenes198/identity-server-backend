using Anjoz.Identity.Application.Dtos.Base;

namespace Anjoz.Identity.Application.Dtos.Identity.Claim
{
    public class ClaimFiltroDto : FiltroDto
    {
        public int? Id { get; set; }
        
        public string Valor { get; set; }

        public string Descricao { get; set; }
    }
}
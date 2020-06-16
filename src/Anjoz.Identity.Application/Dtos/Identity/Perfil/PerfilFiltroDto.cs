using Anjoz.Identity.Application.Dtos.Base;

namespace Anjoz.Identity.Application.Dtos.Identity.Perfil
{
    public class PerfilFiltroDto : FiltroDto
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
    }
}
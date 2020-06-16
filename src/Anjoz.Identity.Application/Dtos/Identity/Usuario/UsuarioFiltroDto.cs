using Anjoz.Identity.Application.Dtos.Base;

namespace Anjoz.Identity.Application.Dtos.Identity.Usuario
{
    public class UsuarioFiltroDto : FiltroDto
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        
        public int? CodigoEquipe { get; set; }
    }
}
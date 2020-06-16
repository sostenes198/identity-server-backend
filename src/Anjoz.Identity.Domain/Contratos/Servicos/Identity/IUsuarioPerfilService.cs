using Anjoz.Identity.Domain.Entidades.Identity;

namespace Anjoz.Identity.Domain.Contratos.Servicos.Identity
{
    public interface IUsuarioPerfilService : IEntidadeVinculoService<UsuarioPerfil, int, Perfil, int>
    {
    }
}
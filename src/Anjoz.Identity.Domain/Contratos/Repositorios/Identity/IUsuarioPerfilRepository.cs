using Anjoz.Identity.Domain.Contratos.Repositorios.Crud;
using Anjoz.Identity.Domain.Entidades.Identity;

namespace Anjoz.Identity.Domain.Contratos.Repositorios.Identity
{
    public interface IUsuarioPerfilRepository : ICrudRepository<UsuarioPerfil, int>
    {
        
    }
}
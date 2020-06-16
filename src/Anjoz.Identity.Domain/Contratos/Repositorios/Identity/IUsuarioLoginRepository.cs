using Anjoz.Identity.Domain.Contratos.Repositorios.Crud;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.EntitiesId.Identity;

namespace Anjoz.Identity.Domain.Contratos.Repositorios.Identity
{
    public interface IUsuarioLoginRepository : ICrudRepository<UsuarioLogin, UserLoginId>
    {
    }
}
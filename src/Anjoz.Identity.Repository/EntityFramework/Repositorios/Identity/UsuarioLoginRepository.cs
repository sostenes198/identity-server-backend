using Anjoz.Identity.Domain.Contratos.Repositorios.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.EntitiesId.Identity;
using Anjoz.Identity.Repository.EntityFramework.Context;
using Anjoz.Identity.Repository.EntityFramework.Repositorios.Crud;

namespace Anjoz.Identity.Repository.EntityFramework.Repositorios.Identity
{
    public class UsuarioLoginRepository : CrudRepository<UsuarioLogin, UserLoginId>, IUsuarioLoginRepository
    {
        public UsuarioLoginRepository(IdentityContext context) : base(context)
        {
        }
    }
}
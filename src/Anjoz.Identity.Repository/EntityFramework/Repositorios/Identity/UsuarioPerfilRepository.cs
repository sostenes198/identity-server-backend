using Anjoz.Identity.Domain.Contratos.Repositorios.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Repository.EntityFramework.Context;
using Anjoz.Identity.Repository.EntityFramework.Repositorios.Crud;

namespace Anjoz.Identity.Repository.EntityFramework.Repositorios.Identity
{
    public class UsuarioPerfilRepository : CrudRepository<UsuarioPerfil, int>,  IUsuarioPerfilRepository
    {
        public UsuarioPerfilRepository(IdentityContext context) : base(context)
        {
        }
    }
}
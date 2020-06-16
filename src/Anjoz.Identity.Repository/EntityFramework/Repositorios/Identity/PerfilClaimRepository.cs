using Anjoz.Identity.Domain.Contratos.Repositorios.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Repository.EntityFramework.Context;
using Anjoz.Identity.Repository.EntityFramework.Repositorios.Crud;

namespace Anjoz.Identity.Repository.EntityFramework.Repositorios.Identity
{
    public class PerfilClaimRepository : CrudRepository<PerfilClaim, int>, IPerfilClaimRepository
    {
        public PerfilClaimRepository(IdentityContext context) : base(context)
        {
        }
    }
}
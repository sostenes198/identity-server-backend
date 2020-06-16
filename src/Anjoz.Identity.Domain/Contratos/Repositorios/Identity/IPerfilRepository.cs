using System.Threading.Tasks;
using Anjoz.Identity.Domain.Entidades.Identity;
using Microsoft.AspNetCore.Identity;

namespace Anjoz.Identity.Domain.Contratos.Repositorios.Identity
{
    public interface IPerfilRepository : IIDentityRepository<Perfil, int>
    {
        Task<IdentityResult> CriarAsync(Perfil perfil);
    }
}
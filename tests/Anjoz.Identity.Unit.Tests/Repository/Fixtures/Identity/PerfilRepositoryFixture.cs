using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Repositorios.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Unit.Tests.Repository.Fixtures.Crud;
using Anjoz.Identity.Utils.Tests.Fakers.Identity;
using Microsoft.AspNetCore.Identity;

namespace Anjoz.Identity.Unit.Tests.Repository.Fixtures.Identity
{
    public class PerfilRepositoryFixture : CrudUnitTestRepositoryFixture
    {
        public async Task<(IdentityResult, Perfil)> GerarPerfilRandomico(IPerfilRepository perfilRepository)
        {
            var perfil = new PerfilFaker().Generate();
            var resultado = await perfilRepository.CriarAsync(perfil);
            return (resultado, perfil);
        }
    }
}
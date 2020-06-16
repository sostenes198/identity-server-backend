using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Repositorios.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Unit.Tests.Repository.Fixtures.Crud;
using Anjoz.Identity.Utils.Tests.Fakers.Identity;
using Microsoft.AspNetCore.Identity;

namespace Anjoz.Identity.Unit.Tests.Repository.Fixtures.Identity
{
    public class UsuarioRepositoryFixture : CrudUnitTestRepositoryFixture
    {
        public async Task<(IdentityResult, Usuario)> GerarUsuarioRandomico(IUsuarioRepository identityRepository)
        {
            var user = new UsuarioFaker().Generate();

            var result = await identityRepository.CriarAsync(user, "123456Abc%&");

            return (result, user);
        }
    }
}
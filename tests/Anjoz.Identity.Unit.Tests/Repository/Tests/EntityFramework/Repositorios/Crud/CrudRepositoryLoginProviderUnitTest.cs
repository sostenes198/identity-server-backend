using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Repositorios.Identity;
using Anjoz.Identity.Domain.EntitiesId.Identity;
using Anjoz.Identity.Unit.Tests.Repository.Fixtures.Crud;
using Anjoz.Identity.Utils.Tests.Fakers;
using Anjoz.Identity.Utils.Tests.Fakers.Identity;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Repository.Tests.EntityFramework.Repositorios.Crud
{
    public class CrudRepositoryLoginProviderUnitTest : IClassFixture<CrudUnitTestRepositoryFixture>
    {
        private readonly IUsuarioRepository _identityRepository;
        private readonly IUsuarioLoginRepository _crudRepository;

        public CrudRepositoryLoginProviderUnitTest(CrudUnitTestRepositoryFixture fixture)
        {
            _identityRepository = fixture.ServiceProvider.GetService<IUsuarioRepository>();
            _crudRepository = fixture.ServiceProvider.GetService<IUsuarioLoginRepository>();
        }

        [Fact]
        public async Task Deve_Obter_Por_Id()
        {
            var usuario = new UsuarioFaker().Generate();
            await _identityRepository.CriarAsync(usuario, "123Ab*&");

            var login = new LoginFaker().Generate();
            login.UserId = usuario.Id;
            await _crudRepository.CriarAsync(login);

            var resultado = await _crudRepository.ObterPorIdAsync(new UserLoginId {LoginProvider = login.LoginProvider, ProviderKey = login.ProviderKey});

            login.Should().BeEquivalentTo(resultado);
        }
    }
}
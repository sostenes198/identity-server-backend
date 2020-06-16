using System.Collections.Generic;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Repositorios.Crud;
using Bogus;

namespace Anjoz.Identity.Unit.Tests.Repository.Fixtures.Crud
{
    public class CrudUnitTestRepositoryFixture : BaseUnitTestRepositoryFixture
    {
        public async Task<TEntidade> CriarEntidadeAsync<TEntidade, TId>(ICrudRepository<TEntidade, TId> crudRepository, Faker<TEntidade> faker)
            where TEntidade : class
        {
            var entidade = faker.Generate();
            await crudRepository.CriarAsync(entidade);

            return entidade;
        }
        
        public async Task<IEnumerable<TEntidade>> CriarEntidadeAsync<TEntidade, TId>(ICrudRepository<TEntidade, TId> crudRepository, Faker<TEntidade> faker, int quantidadeRegistros)
            where TEntidade : class
        {
            var entidades = faker.Generate(quantidadeRegistros);
            await crudRepository.CriarAsync(entidades);

            return entidades;
        }
    }
}
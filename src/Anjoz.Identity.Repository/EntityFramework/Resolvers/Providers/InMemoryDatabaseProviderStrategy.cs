using Anjoz.Identity.Repository.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Anjoz.Identity.Repository.EntityFramework.Resolvers.Providers
{
    public class InMemoryDatabaseProviderStrategy : IDatabaseProviderStrategy
    {
        private const string DatabaseName = "DATABASE_TESTE";

        public void ConfigurarProvedor(DbContextOptionsBuilder builder)
        {
            builder.UseInMemoryDatabase(DatabaseName);
        }
    }
}
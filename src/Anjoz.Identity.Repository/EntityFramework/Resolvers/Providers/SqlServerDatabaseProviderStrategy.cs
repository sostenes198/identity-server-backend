using Anjoz.Identity.Repository.Contratos;
using Anjoz.Identity.Repository.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Anjoz.Identity.Repository.EntityFramework.Resolvers.Providers
{
    public class SqlServerDatabaseProviderStrategy : IDatabaseProviderStrategy
    {
        private readonly IConnectionStringStrategy _connectionStringStrategy;

        public SqlServerDatabaseProviderStrategy(IConnectionStringStrategy connectionStringStrategy)
        {
            _connectionStringStrategy = connectionStringStrategy;
        }
        public void ConfigurarProvedor(DbContextOptionsBuilder builder)
        {
            var connectionString = _connectionStringStrategy.ObterConnectionString();

            builder.UseSqlServer(connectionString, 
                sql => sql.MigrationsAssembly(typeof(IdentityContext).Assembly.GetName().Name));
        }
    }
}
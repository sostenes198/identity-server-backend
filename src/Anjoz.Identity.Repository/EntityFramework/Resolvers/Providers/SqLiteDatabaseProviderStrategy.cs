using System;
using Anjoz.Identity.Repository.Contratos;
using Anjoz.Identity.Repository.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Anjoz.Identity.Repository.EntityFramework.Resolvers.Providers
{
    public class SqLiteDatabaseProviderStrategy : IDatabaseProviderStrategy
    {
        private readonly string ConnectionString = $"DataSource={Guid.NewGuid()}.sqlite";
        
        public void ConfigurarProvedor(DbContextOptionsBuilder builder)
        {
            builder.UseSqlite(ConnectionString,
                sql => sql.MigrationsAssembly(typeof(IdentityContext).Assembly.GetName().Name));
        }
    }
}
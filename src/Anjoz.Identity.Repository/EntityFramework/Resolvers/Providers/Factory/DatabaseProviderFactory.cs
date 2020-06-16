using System;
using Anjoz.Identity.Domain.Excecoes;
using Anjoz.Identity.Domain.Recursos;
using Anjoz.Identity.Repository.Contratos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Anjoz.Identity.Repository.EntityFramework.Resolvers.Providers.Factory
{
    public static class DatabaseProviderFactory
    {
        public const string DatabaseProvider = "DatabaseProvider";

        public static IDatabaseProviderStrategy CriarProvedor(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var databaseProvider = configuration.GetSection(DatabaseProvider).Value;

            switch (databaseProvider)
            {
                case "SQL_SERVER": return serviceProvider.GetService<SqlServerDatabaseProviderStrategy>();
                case "IN_MEMORY": return serviceProvider.GetService<InMemoryDatabaseProviderStrategy>();
                case "SQLITE": return serviceProvider.GetService<SqLiteDatabaseProviderStrategy>();
                default: throw new BusinessException(Mensagens.Provedor_NaoEncontrado);
            }
        }
    }
}
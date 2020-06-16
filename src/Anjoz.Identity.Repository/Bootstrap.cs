using Anjoz.Identity.Domain.Contratos.Repositorios.Crud;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Infrastructure.Configuracoes.Identity;
using Anjoz.Identity.Repository.ConnectionString;
using Anjoz.Identity.Repository.Contratos;
using Anjoz.Identity.Repository.EntityFramework.Context;
using Anjoz.Identity.Repository.EntityFramework.Resolvers.Providers;
using Anjoz.Identity.Repository.EntityFramework.Resolvers.Providers.Factory;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Anjoz.Identity.Repository
{
    public static class Bootstrap
    {
        public static IServiceCollection RegistrarRepositoryServices(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureDbContext(services);
            ConfigureIdentityCore(services, configuration);
            ConfigureRepositoryServices(services, configuration);

            return services;
        }

        private static void ConfigureDbContext(IServiceCollection services)
        {
            services.AddDbContext<IdentityContext>();
        }

        private static void ConfigureIdentityCore(this IServiceCollection services, IConfiguration configuration)
        {
            var identityConfiguration = configuration.GetSection(nameof(IdentityConfiguration)).Get<IdentityConfiguration>();
            services.AddIdentityCore<Usuario>(options =>
                {
                    options.SignIn.RequireConfirmedEmail = identityConfiguration.RequerConfirmacaoEmail;

                    options.Password.RequiredLength = identityConfiguration.SenhaConfiguracao.QuantidadeCaracterMinimo;
                    options.Password.RequireDigit = identityConfiguration.SenhaConfiguracao.RequerDigito;
                    options.Password.RequireLowercase = identityConfiguration.SenhaConfiguracao.RequerLetraMinuscula;
                    options.Password.RequireUppercase = identityConfiguration.SenhaConfiguracao.RequerLetraMaiuscula;
                    options.Password.RequiredUniqueChars = identityConfiguration.SenhaConfiguracao.QuantidadeCaracterUnico;
                    options.Password.RequireNonAlphanumeric = identityConfiguration.SenhaConfiguracao.RequererCaracterEspecial;

                    options.Lockout.AllowedForNewUsers = identityConfiguration.LockoutConfiguracao.PermitirParaNovosUsuarios;
                    options.Lockout.MaxFailedAccessAttempts = identityConfiguration.LockoutConfiguracao.TentativaMaximaErrors;
                })
                .AddRoles<Perfil>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddRoleValidator<RoleValidator<Perfil>>()
                .AddRoleManager<RoleManager<Perfil>>()
                .AddSignInManager<SignInManager<Usuario>>()
                .AddDefaultTokenProviders();
        }

        private static void ConfigureRepositoryServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionStringStrategy, ConnectionStringStrategy>();

            services.AddSingleton<SqlServerDatabaseProviderStrategy>();
            services.AddSingleton<InMemoryDatabaseProviderStrategy>();
            services.AddSingleton<SqLiteDatabaseProviderStrategy>();
            services.AddScoped(provider => DatabaseProviderFactory.CriarProvedor(provider, configuration));

            services.Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(filtro => filtro.AssignableToAny(
                    typeof(ICrudRepository<,>),
                    typeof(IReadRepository<,>),
                    typeof(IWriteRepository<>)
                ))
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }
    }
}
using Anjoz.Identity.Domain.Contratos.Servicos;
using Anjoz.Identity.Domain.Contratos.Servicos.Crud;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Contratos.Validadores;
using Anjoz.Identity.Domain.Servicos;
using Anjoz.Identity.Domain.Servicos.Identity;
using Anjoz.Identity.Domain.Validadores.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Anjoz.Identity.Domain
{
    public static class Bootstrap
    {
        public static IServiceCollection RegistrarDomainServices(this IServiceCollection services, IConfiguration configuracao)
        {
            RegistrarDomainServices(services);
            RegistrarValidadores(services);

            return services;
        }

        private static void RegistrarDomainServices(IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ISignInManagerService, SignInManagerService>();

            services.Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(filtro => filtro.AssignableToAny(
                    typeof(ICrudService<,>),
                    typeof(IReadService<,>),
                    typeof(IWriteService<,>)
                ))
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(filtro => filtro.AssignableToAny(
                    typeof(IEntidadeVinculoService<,,,>)))
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }

        private static void RegistrarValidadores(IServiceCollection services)
        {
            services.AddScoped(typeof(IDomainServiceValidator<>), typeof(DomainServiceValidator<>));
            services.AddScoped(typeof(IServiceValidator<>), typeof(ServiceValidator<>));

            services.Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(filtro => filtro.AssignableToAny(
                    typeof(ISaveServiceValidator<>),
                    typeof(IUpdateServiceValidator<>),
                    typeof(IDeleteServiceValidator<>),
                    typeof(ICustomServiceValidator<>)))
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }
    }
}
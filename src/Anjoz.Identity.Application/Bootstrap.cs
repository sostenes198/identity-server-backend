using Anjoz.Identity.Application.Contratos.Crud;
using Anjoz.Identity.Application.Contratos.Login;
using Anjoz.Identity.Application.Servicos;
using Anjoz.Identity.Application.Token.Jwt;
using Anjoz.Identity.Domain.Contratos.Token;
using Anjoz.Identity.Domain.Entidades.Login;
using Anjoz.Identity.Infrastructure.Interfaces.Filter;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Profile = Anjoz.Identity.Application.AutoMapper.Profiles.Base.Profile;

namespace Anjoz.Identity.Application
{
    public static class Bootstrap
    {
        public static IServiceCollection RegistrarApplicationServices(this IServiceCollection servicos)
        {
            ConfigurarServicos(servicos);
            ConfigurarAutoMapper(servicos);
            ConfigurarGeradoresFiltros(servicos);

            return servicos;
        }

        private static void ConfigurarServicos(IServiceCollection services)
        {
            services.AddSingleton<ITokenProvider<LoginUsuario>, UsuarioTokenJwtProvider>();
            services.AddScoped<ILoginApplicationService, LoginApplicationService>();


            services.Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(filtro => filtro.AssignableToAny(
                    typeof(ICrudApplicationService<,,,,,>),
                    typeof(IReadApplicationService<,,,>),
                    typeof(IWriteApplicationService<,,,,>)
                ))
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }

        private static void ConfigurarAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Profile).Assembly);
        }

        private static void ConfigurarGeradoresFiltros(IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(filtro => filtro.AssignableToAny(
                    typeof(IGeradorFiltro<,>)))
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithSingletonLifetime()
            );
        }
    }
}
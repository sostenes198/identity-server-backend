using Anjoz.Identity.Infrastructure.Configuracoes.Identity;
using Anjoz.Identity.Infrastructure.Configuracoes.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Anjoz.Identity.Infrastructure
{
    public static class Bootstrap
    {
        public static IServiceCollection RegistrarInfrastructureServices(this IServiceCollection servicos, IConfiguration configuracoes)
        {
            ConfigurarOptions(servicos, configuracoes);
            
            return servicos;
        }
        
        public static void ConfigurarOptions(IServiceCollection servicos, IConfiguration configuracao)
        {
            servicos.Configure<IdentityConfiguration>(configuracao.GetSection(nameof(IdentityConfiguration)));
            servicos.Configure<JwtConfiguration>(configuracao.GetSection(nameof(JwtConfiguration)));
        }
    }
}
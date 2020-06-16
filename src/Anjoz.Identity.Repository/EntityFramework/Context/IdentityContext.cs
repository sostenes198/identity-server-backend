using Anjoz.Identity.Domain.Configuracoes;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Repository.Contratos;
using Anjoz.Identity.Repository.EntityFramework.Extensoes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Anjoz.Identity.Repository.EntityFramework.Context
{
    public class IdentityContext : IdentityDbContext<Usuario, Perfil, int,
        UsuarioClaim, UsuarioPerfil, UsuarioLogin, PerfilClaim, UsuarioToken>
    {
        private readonly IDatabaseProviderStrategy _databaseProviderStrategy;

        public IdentityContext(DbContextOptions<IdentityContext> options, IDatabaseProviderStrategy databaseProviderStrategy)
            : base(options)
        {
            _databaseProviderStrategy = databaseProviderStrategy;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("identity");
            
            builder.DefinirTamanhoPadraoPropriedadesString(ConfiguracaoAplicacao.TamanhoMaximoCamposString);
            
            builder.AplicarMapeamentosPorAssembly();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            _databaseProviderStrategy.ConfigurarProvedor(optionsBuilder);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anjoz.Identity.Repository.EntityFramework.Mapeamentos
{
    public class UsuarioLogin : IEntityTypeConfiguration<Domain.Entidades.Identity.UsuarioLogin>
    {
        public void Configure(EntityTypeBuilder<Domain.Entidades.Identity.UsuarioLogin> builder)
        {
            builder.ToTable("usuario_login");

            builder.Property(lnq => lnq.LoginProvider)
                .HasColumnName("provedor_login");
            
            builder.Property(lnq => lnq.ProviderKey)
                .HasColumnName("chave_provedor");
            
            builder.Property(lnq => lnq.ProviderDisplayName)
                .HasColumnName("nome_exibicao_provedor");
            
            builder.Property(lnq => lnq.UserId)
                .HasColumnName("usuario_id");
        }
    }
}
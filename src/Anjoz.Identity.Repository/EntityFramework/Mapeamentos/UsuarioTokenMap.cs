using Anjoz.Identity.Domain.Entidades.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anjoz.Identity.Repository.EntityFramework.Mapeamentos
{
    public class UsuarioTokenMap : IEntityTypeConfiguration<UsuarioToken>
    {
        public void Configure(EntityTypeBuilder<UsuarioToken> builder)
        {
            builder.ToTable("usuario_token");
            
            builder.Property(lnq => lnq.UserId)
                .HasColumnName("usuario_id");
            
            builder.Property(lnq => lnq.LoginProvider)
                .HasColumnName("provedor_login");
            
            builder.Property(lnq => lnq.Name)
                .HasColumnName("nome");
            
            builder.Property(lnq => lnq.Value)
                .HasColumnName("valor");
        }
    }
}
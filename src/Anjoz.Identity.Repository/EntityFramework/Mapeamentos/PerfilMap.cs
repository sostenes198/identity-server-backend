using Anjoz.Identity.Domain.Entidades.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anjoz.Identity.Repository.EntityFramework.Mapeamentos
{
    public class PerfilMap : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.ToTable("perfil");
            
            builder.Property(lnq => lnq.Id)
                .HasColumnName("id");
            
            builder.Property(lnq => lnq.Name)
                .IsRequired()
                .HasColumnName("nome");
            
            builder.Property(lnq => lnq.NormalizedName)
                .IsRequired()
                .HasColumnName("nome_normalizado");
            
            builder.Property(lnq => lnq.ConcurrencyStamp)
                .IsRequired()
                .HasColumnName("concurrency_stamp");
            
            builder.HasIndex(lnq => lnq.Name)
                .IsUnique();
        }
    }
}
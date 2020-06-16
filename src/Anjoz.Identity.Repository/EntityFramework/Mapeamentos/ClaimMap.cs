using Anjoz.Identity.Domain.Entidades.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anjoz.Identity.Repository.EntityFramework.Mapeamentos
{
    public class ClaimMap : IEntityTypeConfiguration<Claim>
    {
        public void Configure(EntityTypeBuilder<Claim> builder)
        {
            
            builder.ToTable("claim");

            builder.HasKey(lnq => lnq.Id);

            builder.Property(lnq => lnq.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(lnq => lnq.Valor)
                .IsRequired()
                .HasColumnName("valor");

            builder.Property(lnq => lnq.ValorNormalizado)
                .IsRequired()
                .HasColumnName("valor_normalizado");
            
            builder.Property(lnq => lnq.Descricao)
                .IsRequired()
                .HasColumnName("descricao");
            
            builder.Property(lnq => lnq.DescricaNormalizada)
                .IsRequired()
                .HasColumnName("descricao_normalizada");
            
            builder.HasIndex(lnq => lnq.Valor)
                .IsUnique();
        }
    }
}
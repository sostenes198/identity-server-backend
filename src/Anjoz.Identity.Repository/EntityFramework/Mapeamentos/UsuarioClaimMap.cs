using Anjoz.Identity.Domain.Entidades.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anjoz.Identity.Repository.EntityFramework.Mapeamentos
{
    public class UsuarioClaimMap : IEntityTypeConfiguration<UsuarioClaim>
    {
        public void Configure(EntityTypeBuilder<UsuarioClaim> builder)
        {
            builder.ToTable("usuario_claim");

            builder.HasKey(lnq => new {lnq.UserId, lnq.ClaimId});

            builder.Property(lnq => lnq.UserId)
                .HasColumnName("usuario_id");

            builder.Property(lnq => lnq.ClaimId)
                .HasColumnName("claim_id");

            builder.HasOne(lnq => lnq.Usuario)
                .WithMany(lnq => lnq.UsuariosClaims)
                .HasForeignKey(lnq => lnq.UserId);

            builder.HasOne(lnq => lnq.Claim)
                .WithMany(lnq => lnq.UsuariosClaims)
                .HasForeignKey(lnq => lnq.ClaimId);

            builder.Ignore(lnq => lnq.Id);
            builder.Ignore(lnq => lnq.ClaimType);
            builder.Ignore(lnq => lnq.ClaimValue);
        }
    }
}
using Anjoz.Identity.Domain.Entidades.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anjoz.Identity.Repository.EntityFramework.Mapeamentos
{
    public class PerfilClaimMap : IEntityTypeConfiguration<PerfilClaim>
    {
        public void Configure(EntityTypeBuilder<PerfilClaim> builder)
        {
            builder.ToTable("perfil_claim");

            builder.HasKey(lnq => new {lnq.RoleId, lnq.ClaimId});

            builder.Property(lnq => lnq.ClaimId)
                .HasColumnName("claim_id");

            builder.Property(lnq => lnq.RoleId)
                .HasColumnName("perfil_id");

            builder.HasOne(lnq => lnq.Perfil)
                .WithMany(lnq => lnq.PerfisClaims)
                .HasForeignKey(lnq => lnq.RoleId);

            builder.HasOne(lnq => lnq.Claim)
                .WithMany(lnq => lnq.PerfisClaims)
                .HasForeignKey(lnq => lnq.ClaimId);

            builder.Ignore(lnq => lnq.Id);
            builder.Ignore(lnq => lnq.ClaimType);
            builder.Ignore(lnq => lnq.ClaimValue);
        }
    }
}
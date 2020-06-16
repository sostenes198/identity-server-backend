using Anjoz.Identity.Domain.Entidades.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anjoz.Identity.Repository.EntityFramework.Mapeamentos
{
    public class UsuarioPefilMap : IEntityTypeConfiguration<UsuarioPerfil>
    {
        public void Configure(EntityTypeBuilder<UsuarioPerfil> builder)
        {
            builder.ToTable("usuario_perfil");

            builder.HasKey(lnq => new {lnq.UserId, lnq.RoleId});

            builder.Property(lnq => lnq.UserId)
                .HasColumnName("usuario_id");

            builder.Property(lnq => lnq.RoleId)
                .HasColumnName("perfil_id");

            builder.HasOne(lnq => lnq.Usuario)
                .WithMany(lnq => lnq.UsuariosPerfis)
                .HasForeignKey(lnq => lnq.UserId);

            builder.HasOne(lnq => lnq.Perfil)
                .WithMany(lnq => lnq.UsuarioPerfis)
                .HasForeignKey(lnq => lnq.RoleId);
        }
    }
}
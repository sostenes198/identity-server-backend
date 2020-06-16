using Anjoz.Identity.Domain.Entidades.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anjoz.Identity.Repository.EntityFramework.Mapeamentos
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario");

            builder.Property(lnq => lnq.Id)
                .HasColumnName("id");
            
            builder.Property(lnq => lnq.Login)
                .IsRequired()
                .HasColumnName("login");
            
            builder.Property(lnq => lnq.LoginNormalizado)
                .IsRequired()
                .HasColumnName("login_normalizado");
            
            builder.Property(lnq => lnq.UserName)
                .IsRequired()
                .HasColumnName("nome_usuario");
            
            builder.Property(lnq => lnq.NormalizedUserName)
                .IsRequired()
                .HasColumnName("nome_usuario_normalizado");
            
            builder.Property(lnq => lnq.Email)
                .HasColumnName("email");
            
            builder.Property(lnq => lnq.NormalizedEmail)
                .HasColumnName("email_normalizado");
            
            builder.Property(lnq => lnq.EmailConfirmed)
                .IsRequired()
                .HasColumnName("email_confirmado");
            
            builder.Property(lnq => lnq.PasswordHash)
                .IsRequired()
                .HasColumnName("senha_hash");
            
            builder.Property(lnq => lnq.PhoneNumber)
                .HasColumnName("telefone");
            
            builder.Property(lnq => lnq.PhoneNumberConfirmed)
                .HasColumnName("telefone_confirmado");

            builder.Property(lnq => lnq.LockoutEnd)
                .HasColumnName("fim_bloqueio");
            
            builder.Property(lnq => lnq.LockoutEnabled)
                .IsRequired()
                .HasColumnName("bloqueio_habilitado");
            
            builder.Property(lnq => lnq.AccessFailedCount)
                .HasColumnName("contagem_falha_acesso");
            
            builder.Property(lnq => lnq.TwoFactorEnabled)
                .IsRequired()
                .HasColumnName("two_factor_enabled");
            
            builder.Property(lnq => lnq.SecurityStamp)
                .IsRequired()
                .HasColumnName("security_stamp");
            
            builder.Property(lnq => lnq.ConcurrencyStamp)
                .IsRequired()
                .HasColumnName("concurrency_stamp");

            builder.Property(lnq => lnq.CodigoEquipe)
                .HasColumnName("codigo_equipe");

            builder.HasIndex(lnq => lnq.Login)
                .IsUnique();

            builder.HasIndex(lnq => lnq.LoginNormalizado)
                .IsUnique();
        }
    }
}
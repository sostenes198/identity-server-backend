using System;
using System.Linq;
using Anjoz.Identity.Repository.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Anjoz.Identity.Repository.EntityFramework.Extensoes
{
    internal static class ModelBuilderExtensions
    {
        public static void DefinirTamanhoPadraoPropriedadesString(this ModelBuilder modelBuilder, int maxLength)
        {
            var propriedadesString = modelBuilder.Model.GetEntityTypes().SelectMany(lnq => lnq.GetProperties()).Where(lnq => lnq.ClrType == typeof(string));
            foreach (var property in propriedadesString)
            {
                if (property.GetMaxLength() == default)
                    property.SetMaxLength(maxLength);
            }
        }

        public static void AplicarMapeamentosPorAssembly(this ModelBuilder modelBuilder)
        {
            var tiposParaRegistrar = typeof(IdentityContext).Assembly.GetTypes()
                .Where(lnq => lnq.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

            foreach (var tipo in tiposParaRegistrar)
            {
                dynamic classeMapeamento = Activator.CreateInstance(tipo);
                modelBuilder.ApplyConfiguration(classeMapeamento);
            }
        }
    }
}
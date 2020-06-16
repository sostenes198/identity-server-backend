using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Anjoz.Identity.Repository.EntityFramework.Extensoes
{
    internal static class DbContextExtensions
    {
        public static Task<T> EncontrarEntidadeAsync<T>(this DbContext context, T entity)
            where T : class
        {
            var primaryKeys = EncontrarValoresChavesPrimarias(context, entity);

            return context.Set<T>().FindAsync(primaryKeys);
        }

        public static object[] EncontrarValoresChavesPrimarias<T>(this DbContext context, T entity)
            where T : class
        {
            return context.EncontrarPropriedadesChavesPrimarias<T>()
                .Select(lnq => entity.ObterValorPropriedade(lnq.Name)).ToArray();
        }

        private static IReadOnlyList<IProperty> EncontrarPropriedadesChavesPrimarias<T>(this DbContext context)
            where T : class
        {
            return context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties;
        }
        
        private static object ObterValorPropriedade<T>(this T entity, string name)
        {
            return entity.GetType().GetProperty(name)?.GetValue(entity, null);
        }
    }
}
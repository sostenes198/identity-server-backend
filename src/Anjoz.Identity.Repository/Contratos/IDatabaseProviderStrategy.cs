using Microsoft.EntityFrameworkCore;

namespace Anjoz.Identity.Repository.Contratos
{
    public interface IDatabaseProviderStrategy
    {
        void ConfigurarProvedor(DbContextOptionsBuilder builder);
    }
}
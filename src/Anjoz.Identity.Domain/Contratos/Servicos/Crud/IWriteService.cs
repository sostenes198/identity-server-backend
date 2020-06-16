using System.Threading.Tasks;

namespace Anjoz.Identity.Domain.Contratos.Servicos.Crud
{
    public interface IWriteService<in T, in TId>
        where T : class
    {
        Task CriarAsync(T entidade);
        Task AtualizarAsync(T entidade);
        Task ExcluirAsync(TId id);
    }
}
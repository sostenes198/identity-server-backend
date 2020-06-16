using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anjoz.Identity.Domain.Contratos.Repositorios.Crud
{
    public interface IWriteRepository<T>
        where T : class
    {
        Task CriarAsync(T entidade);
        Task CriarAsync(ICollection<T> entidades);
        Task AtualizarAsync(T entidade);
        Task ExcluirAsync(T entidade);
        Task ExcluirAsync(ICollection<T> entidades);
    }
}
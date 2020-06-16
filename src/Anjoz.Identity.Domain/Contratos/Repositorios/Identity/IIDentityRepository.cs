using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Repositorios.Crud;
using Microsoft.AspNetCore.Identity;

namespace Anjoz.Identity.Domain.Contratos.Repositorios.Identity
{
    public interface IIDentityRepository<TEntidade, in TId> : IReadRepository<TEntidade, TId>
        where TEntidade : class
    {
        Task<TEntidade> ObterPorNomeAsync(string nome, string[] includes = default);
        Task<IdentityResult> AtualizarAsync(TEntidade entidade);
        Task<IdentityResult> ExcluirAsync(TEntidade entidade);
    }
}
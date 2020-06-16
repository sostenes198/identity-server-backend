using System.Threading.Tasks;
using Anjoz.Identity.Domain.Entidades.Identity;
using Microsoft.AspNetCore.Identity;

namespace Anjoz.Identity.Domain.Contratos.Repositorios.Identity
{
    public interface IUsuarioRepository : IIDentityRepository<Usuario, int>
    {
        Task<IdentityResult> CriarAsync(Usuario usuario, string password);
        Task<Usuario> ObterPorLoginAsync(string login, string[] includes = default);
        Task<IdentityResult> AtualizarSenha(Usuario usuario, string novaSenha);
    }
}
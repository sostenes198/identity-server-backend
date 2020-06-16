using System.Threading.Tasks;
using Anjoz.Identity.Domain.Entidades.Identity;

namespace Anjoz.Identity.Domain.Contratos.Servicos.Identity
{
    public interface ISignInManagerService

    {
    Task<bool> ChecarSenhaUsuario(Usuario usuario, string senha, bool travarAoFalhar = false);
    }
}
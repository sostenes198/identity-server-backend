using System.Threading.Tasks;
using Anjoz.Identity.Domain.Entidades.Login;

namespace Anjoz.Identity.Domain.Contratos.Servicos
{
    public interface ILoginService
    {
        Task<LoginUsuario> Login(Login login);
    }
}
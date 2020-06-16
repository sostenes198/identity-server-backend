using System.Threading.Tasks;
using Anjoz.Identity.Application.Dtos.Login;

namespace Anjoz.Identity.Application.Contratos.Login
{
    public interface ILoginApplicationService
    {
        Task<LoginUsuarioDto> Login(LoginDto loginDto);
    }
}
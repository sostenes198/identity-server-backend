using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Microsoft.AspNetCore.Identity;

namespace Anjoz.Identity.Domain.Servicos.Identity
{
    public class SignInManagerService : ISignInManagerService
    {
        private readonly SignInManager<Usuario> _signInManager;

        public SignInManagerService(SignInManager<Usuario> signInManager)
        {
            _signInManager = signInManager;
        }
        public Task<bool> ChecarSenhaUsuario(Usuario usuario, string senha, bool travarAoFalhar = false)
        {
            return _signInManager.CheckPasswordSignInAsync(usuario, senha, travarAoFalhar)
                .ContinueWith(task => task.Result.Succeeded);
        }
    }
}
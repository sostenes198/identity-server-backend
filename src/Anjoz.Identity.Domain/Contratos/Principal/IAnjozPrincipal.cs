using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace Anjoz.Identity.Domain.Contratos.Principal
{
    public interface IAnjozPrincipal : IIdentity
    {
        string Id { get; }
        string Nome { get; }
        string Email { get; }
        ICollection<string> Permissoes { get; }
        
        IEnumerable<Claim> Claims { get; }

        bool TemPermissao(params string[] valoresPermissoes);
    }
}
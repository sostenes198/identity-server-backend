using System.Collections.Generic;

namespace Anjoz.Identity.Application.Dtos.Login
{
    public class LoginUsuarioDto
    {
        public LoginUsuarioDto()
        {
            Claims = new HashSet<LoginClaimDto>();
        }
        
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string AcessToken { get; set; }
        public IEnumerable<LoginClaimDto> Claims { get; set; }
    }
}
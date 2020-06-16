using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Servicos;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Contratos.Token;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Entidades.Login;
using Anjoz.Identity.Domain.Excecoes;
using Anjoz.Identity.Domain.Recursos;

namespace Anjoz.Identity.Domain.Servicos
{
    public class LoginService : ILoginService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ISignInManagerService _signInManagerService;
        private readonly ITokenProvider<LoginUsuario> _tokenProvider;
        private readonly IUsuarioClaimService _entidadeVinculoService;
        private readonly IClaimService _claimService;

        public LoginService(IUsuarioService usuarioService, 
            ISignInManagerService signInManagerService,
            ITokenProvider<LoginUsuario> tokenProvider,
            IUsuarioClaimService entidadeVinculoService,
            IClaimService claimService)
        {
            _usuarioService = usuarioService;
            _signInManagerService = signInManagerService;
            _tokenProvider = tokenProvider;
            _entidadeVinculoService = entidadeVinculoService;
            _claimService = claimService;
        }

        public async Task<LoginUsuario> Login(Login login)
        {
            var usuarioAplicacao = await TryObterUsuarioAplicacao(login);
            var claimsUsuario = await ListarTodasClaimsUsuario(usuarioAplicacao);
            var loginUsuario = LoginUsuario.Inicializar(usuarioAplicacao, claimsUsuario);

            loginUsuario.AcessToken = await _tokenProvider.GerarToken(loginUsuario);

            return loginUsuario;
        }

        private async Task<Usuario> TryObterUsuarioAplicacao(Login login)
        {
            var userApplication = await _usuarioService.ObterPorLoginAsync(login.LoginUsuario);

            if (userApplication == default)
                throw new NotFoundException(Mensagens.Login_NomeOuSenhaInvalido);
            
            var passwordUserCorrect = await _signInManagerService.ChecarSenhaUsuario(userApplication, login.Senha);
            
            if (passwordUserCorrect == false)
                throw new NotFoundException(Mensagens.Login_NomeOuSenhaInvalido);

            return userApplication;
        }
        
        private async Task<IEnumerable<Claim>> ListarTodasClaimsUsuario(Usuario usuario)
        {
            var claimsId = (await _entidadeVinculoService.ListarPorAsync(lnq => lnq.UserId == usuario.Id))
                .Select(lnq => lnq.ClaimId).ToArray();
            return claimsId.Any() ? (await _claimService.ListarPorAsync(lnq => claimsId.Contains(lnq.Id))) as IList<Claim> : new List<Claim>();
        }
    }
}
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Repositorios.Identity;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Excecoes;
using Anjoz.Identity.Domain.Extensoes.Identity.Usuario;
using Anjoz.Identity.Domain.Extensoes.Paginacao;
using Anjoz.Identity.Domain.Recursos;
using Microsoft.AspNetCore.Identity;

namespace Anjoz.Identity.Domain.Servicos.Identity
{
    public class PerfilService : IPerfilService
    {
        private readonly IPerfilRepository _perfilRepository;
        private readonly IPerfilClaimService _perfilClaimVinculoService;

        public PerfilService(IPerfilRepository perfilRepository,
            IPerfilClaimService perfilClaimVinculoService)
        {
            _perfilRepository = perfilRepository;
            _perfilClaimVinculoService = perfilClaimVinculoService;
        }

        public Task<Perfil> ObterPorNomeAsync(string nome, string[] includes = default)
            => _perfilRepository.ObterPorNomeAsync(nome, includes);

        public Task<IPagedList<Claim>> ListarClaims(int idPerfil, IPagedParam pagedParam = default)
        {
            return _perfilClaimVinculoService.ListarTodosVinculosEntidade(idPerfil, new[] {nameof(PerfilClaim.Claim)}, pagedParam)
                .ContinueWith(tsk => tsk.Result.Select(lnq => lnq.Claim).ToPagedList(pagedParam));
        }

        public Task<Perfil> ObterPorIdAsync(int id) 
            => _perfilRepository.ObterPorIdAsync(id);

        public Task<IPagedList<Perfil>> ListarPorAsync(Expression<Func<Perfil, bool>> where = default, string[] includes = default, IPagedParam pagedParam = default) 
            => _perfilRepository.ListarPorAsync(where, includes, pagedParam);

        public async Task CriarAsync(Perfil perfil)
        {
            var claimsId = PerfilExtension.ObterPerfilClaimELimpar(perfil).Select(lnq => lnq.ClaimId).ToArray();
            
            var identityResultado = await _perfilRepository.CriarAsync(perfil);

            if (identityResultado.Succeeded == false)
                GerarBusinessExceptionSeResultadoInvalido(identityResultado);

            await _perfilClaimVinculoService.AdicionarVinculo(perfil.Id, claimsId);
        }

        public async Task AtualizarAsync(Perfil perfil)
        {
            var perfilAplicacao = await TentarObterPerfil(perfil.Id);

            AtualizarValoresPerfil(perfilAplicacao, perfil);

            var identityResultado = await _perfilRepository.AtualizarAsync(perfilAplicacao);
            if (identityResultado.Succeeded == false)
                GerarBusinessExceptionSeResultadoInvalido(identityResultado);


            var claimsId = perfil.PerfisClaims.Select(lnq => lnq.ClaimId).ToArray();

            await _perfilClaimVinculoService.AtualizarVinculos(perfilAplicacao.Id, claimsId);
        }

        public async Task ExcluirAsync(int id)
        {
            var perfilAplicacao = await TentarObterPerfil(id);

            var identityResultado = await _perfilRepository.ExcluirAsync(perfilAplicacao);
            if (identityResultado.Succeeded == false)
                GerarBusinessExceptionSeResultadoInvalido(identityResultado);
        }

        private async Task<Perfil> TentarObterPerfil(int id)
        {
            var perfilAplicacao = await _perfilRepository.ObterPorIdAsync(id);

            if (perfilAplicacao == default)
                throw new NotFoundException(Mensagens.Perfil_NaoEncontrado);

            return perfilAplicacao;
        }

        private void AtualizarValoresPerfil(Perfil perfilAplicacao, Perfil perfil)
        {
            perfilAplicacao.Name = perfil.Name;
        }

        private void GerarBusinessExceptionSeResultadoInvalido(IdentityResult resultado)
        {
            throw new BusinessException(resultado.Errors.Select(lnq => lnq.Description).ToList());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Repositorios.Identity;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Contratos.Validadores;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Excecoes;
using Anjoz.Identity.Domain.Extensoes.Identity.Usuario;
using Anjoz.Identity.Domain.Extensoes.Paginacao;
using Anjoz.Identity.Domain.Recursos;
using Anjoz.Identity.Domain.VO;
using Microsoft.AspNetCore.Identity;

namespace Anjoz.Identity.Domain.Servicos.Identity
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioClaimService _usuarioClaimVinculoService;
        private readonly IUsuarioPerfilService _usuarioPerfilVinculoService;
        private readonly IDomainServiceValidator<Usuario> _validador;
        private readonly ICustomServiceValidator<UsuarioAlteracaoSenhaVo> _usuarioAlteracaoSenhaCustomValidator;
        private readonly ISignInManagerService _signInManagerService;

        public UsuarioService(IUsuarioRepository usuarioRepository,
            IUsuarioClaimService usuarioClaimVinculoService,
            IUsuarioPerfilService usuarioPerfilVinculoService,
            IDomainServiceValidator<Usuario> validador,
            ICustomServiceValidator<UsuarioAlteracaoSenhaVo> usuarioAlteracaoSenhaCustomValidator,
            ISignInManagerService signInManagerService
        )
        {
            _usuarioRepository = usuarioRepository;
            _usuarioClaimVinculoService = usuarioClaimVinculoService;
            _usuarioPerfilVinculoService = usuarioPerfilVinculoService;
            _validador = validador;
            _usuarioAlteracaoSenhaCustomValidator = usuarioAlteracaoSenhaCustomValidator;
            _signInManagerService = signInManagerService;
        }

        public Task<Usuario> ObterPorIdAsync(int id)
            => _usuarioRepository.ObterPorIdAsync(id);

        public Task<Usuario> ObterPorLoginAsync(string login, string[] includes = default)
            => _usuarioRepository.ObterPorLoginAsync(login, includes);

        public Task<Usuario> ObterPorNomeAsync(string nome, string[] includes = default)
            => _usuarioRepository.ObterPorNomeAsync(nome, includes);

        public Task<IPagedList<Claim>> ListarClaims(int idUsuario, IPagedParam pagedParam = default)
        {
            return _usuarioClaimVinculoService.ListarTodosVinculosEntidade(idUsuario, new[] {nameof(UsuarioClaim.Claim)}, pagedParam)
                .ContinueWith(tsk => tsk.Result.Select(lnq => lnq.Claim).ToPagedList(pagedParam));
        }

        public async Task AlterarSenha(UsuarioAlteracaoSenhaVo usuarioAlteracaoSenha)
        {
            _usuarioAlteracaoSenhaCustomValidator.ValidarEntidade(usuarioAlteracaoSenha);
            var usuarioAplicacao = await TentarObterUsuariosAplicacao(usuarioAlteracaoSenha.Id);
            await ValidarSenha(usuarioAplicacao, usuarioAlteracaoSenha.SenhaAtual);
            await _usuarioRepository.AtualizarSenha(usuarioAplicacao, usuarioAlteracaoSenha.NovaSenha);
        }

        private async Task ValidarSenha(Usuario usuario, string senha)
        {
            async Task<bool> SenhaInvalida() => await _signInManagerService.ChecarSenhaUsuario(usuario, senha) == false;
            if (await SenhaInvalida())
                throw new BusinessException(Mensagens.Usuario_SenhaAtualInformadaInvalida);
        }

        public Task<IPagedList<Usuario>> ListarPorAsync(Expression<Func<Usuario, bool>> where = default, string[] includes = default, IPagedParam pagedParam = default)
            => _usuarioRepository.ListarPorAsync(where, includes, pagedParam);

        public async Task CriarAsync(Usuario entidade)
        {
            _validador.Salvar.ValidarEntidade(entidade);

            var senha = UsuarioExtension.ObterSenhaELimpar(entidade);
            var usuarioClaims = UsuarioExtension.ObterUsuarioClaimELimpar(entidade);
            var usuarioPerfis = UsuarioExtension.ObterUsuarioPerfilELimpar(entidade);

            var resultado = await _usuarioRepository.CriarAsync(entidade, senha);
            if (resultado.Succeeded == false)
                GerarBusinessException(resultado.Errors);

            await AdicionarClaimsAoUsuario(entidade.Id, usuarioClaims.ToList());
            await AdicionarPerfisAoUsuario(entidade.Id, usuarioPerfis.ToList());
        }

        public async Task AtualizarAsync(Usuario entidade)
        {
            _validador.Atualizar.ValidarEntidade(entidade);

            var usuarioAplicacao = await TentarObterUsuariosAplicacao(entidade.Id);

            UsuarioExtension.AtualizarValores(entidade, usuarioAplicacao);

            var resultado = await _usuarioRepository.AtualizarAsync(usuarioAplicacao);

            if (resultado.Succeeded == false)
                GerarBusinessException(resultado.Errors);

            await AtualizarClaims(entidade.Id, entidade.UsuariosClaims.ToList());
            await AtualizarPerfis(entidade.Id, entidade.UsuariosPerfis.ToList());
        }

        public async Task ExcluirAsync(int id)
        {
            var usuarioAplicacao = await TentarObterUsuariosAplicacao(id);

            var resultado = await _usuarioRepository.ExcluirAsync(usuarioAplicacao);

            if (resultado.Succeeded == false)
                GerarBusinessException(resultado.Errors);
        }

        private void GerarBusinessException(IEnumerable<IdentityError> erros) =>
            throw new BusinessException(erros.Select(lnq => lnq.Description).ToList());

        private async Task AdicionarClaimsAoUsuario(int usuarioId, ICollection<UsuarioClaim> usuarioClaims)
        {
            var claimsId = usuarioClaims.Select(lnq => lnq.ClaimId).ToArray();
            await _usuarioClaimVinculoService.AdicionarVinculo(usuarioId, claimsId);
        }

        private async Task AdicionarPerfisAoUsuario(int usuarioId, ICollection<UsuarioPerfil> usuarioPerfis)
        {
            var perfisId = usuarioPerfis.Select(lnq => lnq.RoleId).ToArray();
            await _usuarioPerfilVinculoService.AdicionarVinculo(usuarioId, perfisId);
        }

        private async Task AtualizarClaims(int usuarioId, ICollection<UsuarioClaim> usuarioClaims)
        {
            var claimsId = usuarioClaims.Select(lnq => lnq.ClaimId).ToArray();
            await _usuarioClaimVinculoService.AtualizarVinculos(usuarioId, claimsId);
        }

        private async Task AtualizarPerfis(int usuarioId, ICollection<UsuarioPerfil> usuarioPerfis)
        {
            var perfisId = usuarioPerfis.Select(lnq => lnq.RoleId).ToArray();
            await _usuarioPerfilVinculoService.AtualizarVinculos(usuarioId, perfisId);
        }

        private async Task<Usuario> TentarObterUsuariosAplicacao(int id)
        {
            var usuarioAplicacao = await _usuarioRepository.ObterPorIdAsync(id);

            if (usuarioAplicacao == default)
                throw new NotFoundException(Mensagens.Usuario_NaoEncontrado);

            return usuarioAplicacao;
        }
    }
}
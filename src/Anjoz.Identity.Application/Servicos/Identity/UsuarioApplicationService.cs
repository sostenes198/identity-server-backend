using System.Threading.Tasks;
using Anjoz.Identity.Application.Contratos.Identity;
using Anjoz.Identity.Application.Dtos.Base;
using Anjoz.Identity.Application.Dtos.Identity.Claim;
using Anjoz.Identity.Application.Dtos.Identity.Usuario;
using Anjoz.Identity.Application.Dtos.Identity.Usuario.AlteracaoSenha;
using Anjoz.Identity.Application.Dtos.Paginacao;
using Anjoz.Identity.Application.Servicos.Crud;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Recursos;
using Anjoz.Identity.Domain.VO;
using Anjoz.Identity.Infrastructure.Interfaces.Filter;
using AutoMapper;

namespace Anjoz.Identity.Application.Servicos.Identity
{
    public class UsuarioApplicationService : CrudApplicationService<UsuarioDto, int, Usuario, UsuarioFiltroDto, UsuarioCriarDto, UsuarioAtualizarDto>,
        IUsuarioApplicationService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuarioApplicationService(
            IUsuarioService usuarioService,
            IMapper mapper, IGeradorFiltro<Usuario, UsuarioFiltroDto> geradorFiltro)
            : base(usuarioService, mapper, geradorFiltro)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        public async Task<UsuarioDto> ObterPorNomeAsync(string nome)
        {
            var resultado = await _usuarioService.ObterPorNomeAsync(nome);
            return _mapper.Map<Usuario, UsuarioDto>(resultado);
        }

        public Task<PagedListDto<ClaimDto>> ListarClaims(int idUsuario, PagedParamFiltroDto pagedParam = default)
        {
            return _usuarioService.ListarClaims(idUsuario, MappePageParam(pagedParam))
                .ContinueWith(tsk => _mapper.Map<IPagedList<Claim>, PagedListDto<ClaimDto>>(tsk.Result));
        }

        public Task<UsuarioAlteracaoSenhaResultadoDto> AlterarSenha(UsuarioAlteracaoSenhaDto usuarioAlteracaoSenha)
        {
            return _usuarioService.AlterarSenha(_mapper.Map<UsuarioAlteracaoSenhaDto, UsuarioAlteracaoSenhaVo>(usuarioAlteracaoSenha))
                .ContinueWith(_ => new UsuarioAlteracaoSenhaResultadoDto {Mensagem = Mensagens.Usuario_SenhaAlteradaComSucesso},
                    TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}
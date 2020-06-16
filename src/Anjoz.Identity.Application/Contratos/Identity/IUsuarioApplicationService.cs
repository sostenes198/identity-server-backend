using System.Threading.Tasks;
using Anjoz.Identity.Application.Contratos.Crud;
using Anjoz.Identity.Application.Dtos.Base;
using Anjoz.Identity.Application.Dtos.Identity.Claim;
using Anjoz.Identity.Application.Dtos.Identity.Usuario;
using Anjoz.Identity.Application.Dtos.Identity.Usuario.AlteracaoSenha;
using Anjoz.Identity.Application.Dtos.Paginacao;
using Anjoz.Identity.Domain.Entidades.Identity;

namespace Anjoz.Identity.Application.Contratos.Identity
{
    public interface IUsuarioApplicationService : ICrudApplicationService<UsuarioDto, int, Usuario, UsuarioFiltroDto, UsuarioCriarDto, UsuarioAtualizarDto>
    {
        Task<UsuarioDto> ObterPorNomeAsync(string nome);

        Task<PagedListDto<ClaimDto>> ListarClaims(int idUsuario, PagedParamFiltroDto pagedParam = default);
        
        Task<UsuarioAlteracaoSenhaResultadoDto> AlterarSenha(UsuarioAlteracaoSenhaDto usuarioAlteracaoSenha);
    }
}
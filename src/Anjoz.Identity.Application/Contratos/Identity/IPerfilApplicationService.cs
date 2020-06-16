using System.Threading.Tasks;
using Anjoz.Identity.Application.Contratos.Crud;
using Anjoz.Identity.Application.Dtos.Base;
using Anjoz.Identity.Application.Dtos.Identity.Claim;
using Anjoz.Identity.Application.Dtos.Identity.Perfil;
using Anjoz.Identity.Application.Dtos.Paginacao;
using Anjoz.Identity.Domain.Entidades.Identity;

namespace Anjoz.Identity.Application.Contratos.Identity
{
    public interface IPerfilApplicationService : ICrudApplicationService<PerfilDto, int, Perfil, PerfilFiltroDto, PerfilCriarDto, PerfilAtualizarDto>
    {
        Task<PerfilDto> ObterPorNomeAsync(string nome);
        
        Task<PagedListDto<ClaimDto>> ListarClaims(int idPerfil, PagedParamFiltroDto pagedParam = default);
    }
}
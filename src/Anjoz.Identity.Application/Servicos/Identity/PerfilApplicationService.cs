using System.Threading.Tasks;
using Anjoz.Identity.Application.Contratos.Identity;
using Anjoz.Identity.Application.Dtos.Base;
using Anjoz.Identity.Application.Dtos.Identity.Claim;
using Anjoz.Identity.Application.Dtos.Identity.Perfil;
using Anjoz.Identity.Application.Dtos.Paginacao;
using Anjoz.Identity.Application.Servicos.Crud;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Infrastructure.Interfaces.Filter;
using AutoMapper;

namespace Anjoz.Identity.Application.Servicos.Identity
{
    public class PerfilApplicationService : CrudApplicationService<PerfilDto, int, Perfil, PerfilFiltroDto, PerfilCriarDto, PerfilAtualizarDto>, IPerfilApplicationService
    {
        private readonly IPerfilService _perfilService;
        private readonly IMapper _mapper;

        public PerfilApplicationService(IPerfilService perfilService, IMapper mapper, IGeradorFiltro<Perfil, PerfilFiltroDto> geradorFiltro) : base(perfilService, mapper, geradorFiltro)
        {
            _perfilService = perfilService;
            _mapper = mapper;
        }

        public Task<PerfilDto> ObterPorNomeAsync(string nome)
        {
            return _perfilService.ObterPorNomeAsync(nome)
                .ContinueWith(tsk => _mapper.Map<Perfil, PerfilDto>(tsk.Result));
        }

        public Task<PagedListDto<ClaimDto>> ListarClaims(int idPerfil, PagedParamFiltroDto pagedParam = default)
        {
            return _perfilService.ListarClaims(idPerfil, MappePageParam(pagedParam))
                .ContinueWith(tsk => _mapper.Map<IPagedList<Claim>, PagedListDto<ClaimDto>>(tsk.Result));
        }
        
        
    }
}
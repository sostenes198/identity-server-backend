using Anjoz.Identity.Application.AutoMapper.Profiles.Base;
using Anjoz.Identity.Application.Dtos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Paginacao;

namespace Anjoz.Identity.Application.AutoMapper.Profiles.Paginacao
{
    public class PagedParamProfile : Profile
    {
        public PagedParamProfile()
        {
            CreateMap<PagedParamFiltroDto, IPagedParam>()
                .ForMember(dest => dest.TotalPages, opt => opt.Ignore());
        }
    }
}
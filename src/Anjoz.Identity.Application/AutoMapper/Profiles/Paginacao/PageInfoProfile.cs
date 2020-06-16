using Anjoz.Identity.Application.AutoMapper.Profiles.Base;
using Anjoz.Identity.Application.Dtos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Paginacao;

namespace Anjoz.Identity.Application.AutoMapper.Profiles.Paginacao
{
    public class PageInfoProfile : Profile
    {
        public PageInfoProfile()
        {
            CreateMap<IPageInfo, PageInfoDto>();          
        }
    }
}
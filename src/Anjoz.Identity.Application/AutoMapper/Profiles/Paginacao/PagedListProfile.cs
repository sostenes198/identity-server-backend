using System.Collections.Generic;
using System.Linq;
using Anjoz.Identity.Application.Dtos.Base;
using Anjoz.Identity.Application.Dtos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using AutoMapper;
using Profile = Anjoz.Identity.Application.AutoMapper.Profiles.Base.Profile;

namespace Anjoz.Identity.Application.AutoMapper.Profiles.Paginacao
{
    public class PagedListProfile : Profile
    {
        public PagedListProfile()
        {
            CreateMap(typeof(IPagedList<>), typeof(PagedListDto<>)).ConvertUsing(typeof(PagedListConverter<,>));
        }
    }

    class PagedListConverter<TEntity, TDto> : ITypeConverter<IPagedList<TEntity>, PagedListDto<TDto>>
        where TDto : EntidadeDto
    {
        public PagedListDto<TDto> Convert(IPagedList<TEntity> source, PagedListDto<TDto> destination, ResolutionContext context)
        {
            return new PagedListDto<TDto>(context.Mapper.Map<IEnumerable<TEntity>, IEnumerable<TDto>>(source).ToList(), context.Mapper.Map<IPageInfo, PageInfoDto>(source.PageInfo));
        }
    }
}
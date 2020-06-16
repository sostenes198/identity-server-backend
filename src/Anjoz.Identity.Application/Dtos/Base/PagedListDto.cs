using System.Collections.Generic;
using Anjoz.Identity.Application.Dtos.Paginacao;

namespace Anjoz.Identity.Application.Dtos.Base
{
    public class PagedListDto<TDto> : EntidadeDto
        where TDto : EntidadeDto
    {
        public PagedListDto()
        {
            
        }
        
        public PagedListDto(ICollection<TDto> items, PageInfoDto pageInfo)
        {
            Items = items;
            Page = pageInfo.PageNumber;
            PageSize = pageInfo.PageSize;
            TotalPages = pageInfo.TotalPages;
            HasPrevious = pageInfo.HasPrevious;
            HasNext = pageInfo.HasNext;
        }

        public ICollection<TDto> Items { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public bool HasPrevious { get; set; }

        public bool HasNext { get; set; }
    }
}
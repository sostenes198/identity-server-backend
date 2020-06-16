namespace Anjoz.Identity.Application.Dtos.Paginacao
{
    public class PageInfoDto
    {
        public int TotalPages { get; set; }
        
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public bool HasPrevious { get; set; }

        public bool HasNext { get; set; }
    }
}
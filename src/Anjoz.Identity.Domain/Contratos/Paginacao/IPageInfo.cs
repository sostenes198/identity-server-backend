namespace Anjoz.Identity.Domain.Contratos.Paginacao
{
    public interface IPageInfo
    {
        int TotalPages { get; set; }
        
        int PageNumber { get; set; }

        int PageSize { get; set; }

        bool HasPrevious { get; }

        bool HasNext { get;  }
    }
}
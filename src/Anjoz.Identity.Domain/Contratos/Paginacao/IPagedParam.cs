namespace Anjoz.Identity.Domain.Contratos.Paginacao
{
    public interface IPagedParam
    {
        int TotalPages { get; set; }
       
        int PageSize { get; set; }

        int PageNumber { get; set; }
    }
}
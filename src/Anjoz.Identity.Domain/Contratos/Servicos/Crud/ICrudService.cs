namespace Anjoz.Identity.Domain.Contratos.Servicos.Crud
{
    public interface ICrudService<T, TId> : IReadService<T, TId>, IWriteService<T, TId>
        where T : class
    {
    }
}
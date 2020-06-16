namespace Anjoz.Identity.Domain.Contratos.Repositorios.Crud
{
    public interface ICrudRepository<T, in TId> : IReadRepository<T, TId>, IWriteRepository<T>
        where T : class
    {
    }
}
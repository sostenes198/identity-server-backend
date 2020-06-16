namespace Anjoz.Identity.Domain.Contratos.Validadores
{
    public interface IDeleteServiceValidator<in TEntidade> : IServiceValidator<TEntidade>
        where TEntidade : class
    {
        
    }
}
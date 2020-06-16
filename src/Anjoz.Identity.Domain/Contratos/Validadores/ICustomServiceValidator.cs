namespace Anjoz.Identity.Domain.Contratos.Validadores
{
    public interface ICustomServiceValidator<in TEntidade> : IServiceValidator<TEntidade>
        where TEntidade : class
    {
        
    }
}
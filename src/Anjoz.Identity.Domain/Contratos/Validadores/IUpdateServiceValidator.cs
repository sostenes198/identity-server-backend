namespace Anjoz.Identity.Domain.Contratos.Validadores
{
    public interface IUpdateServiceValidator<in TEntidade> : IServiceValidator<TEntidade>
        where TEntidade : class
    {
        
    }
}
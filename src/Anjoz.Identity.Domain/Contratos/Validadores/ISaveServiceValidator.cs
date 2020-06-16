namespace Anjoz.Identity.Domain.Contratos.Validadores
{
    public interface ISaveServiceValidator<in TEntidade> : IServiceValidator<TEntidade>
        where TEntidade : class
    {
        
    }
}
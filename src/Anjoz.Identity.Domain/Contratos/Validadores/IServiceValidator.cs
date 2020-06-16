namespace Anjoz.Identity.Domain.Contratos.Validadores
{
    public interface IServiceValidator<in TEntidade>
        where TEntidade : class
    {
        void ValidarEntidade(TEntidade entidade);
    }
}
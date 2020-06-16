namespace Anjoz.Identity.Domain.Contratos.Validadores
{
    public interface IDomainServiceValidator<TEntidade>
        where TEntidade : class
    {
        ISaveServiceValidator<TEntidade> Salvar { get; set; }
        IUpdateServiceValidator<TEntidade> Atualizar { get; set; }
        IDeleteServiceValidator<TEntidade> Deletar { get; set; }
        ICustomServiceValidator<TEntidade> Customizado { get; set; }
    }
}
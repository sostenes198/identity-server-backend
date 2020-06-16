using Anjoz.Identity.Application.Dtos.Base;

namespace Anjoz.Identity.Application.Contratos.Crud
{
    public interface ICrudApplicationService<TDto, TId, TEntidade, in TFiltroDto, in TCriarDto, in TAtualizarDto> :
        IReadApplicationService<TDto, TEntidade, TId, TFiltroDto>,
        IWriteApplicationService<TDto, TId, TEntidade, TCriarDto, TAtualizarDto>
        where TDto : EntidadeDto
        where TEntidade : class
        where TFiltroDto : FiltroDto
        where TCriarDto : CriarEntidadeDto
        where TAtualizarDto : AtualizarEntidadeDto
    {
    }
}
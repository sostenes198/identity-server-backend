using System.Threading.Tasks;
using Anjoz.Identity.Application.Dtos.Base;

namespace Anjoz.Identity.Application.Contratos.Crud
{
    public interface IWriteApplicationService<TDto, TId, in TEntidade, in TCriarDto, in TAtualizarDto>
        where TDto : EntidadeDto
        where TEntidade : class
        where TCriarDto : CriarEntidadeDto
        where TAtualizarDto : AtualizarEntidadeDto
    {
        Task<TDto> CriarAsync(TCriarDto criarDto);
        Task<TDto> AtualizarAsync(TAtualizarDto atualizarDto);
        Task<TId> ExcluirAsync(TId id);
    }
}
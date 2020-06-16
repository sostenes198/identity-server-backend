using System.Collections.Generic;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Servicos.Crud;

namespace Anjoz.Identity.Domain.Contratos.Servicos.Identity
{
    public interface IEntidadeVinculoService<TEntidade, TEntidadeId, TVinculoEntidade, TVinculoId> : ICrudService<TEntidade, TEntidadeId>
        where TEntidade : class
    {
        Task AdicionarVinculo(TEntidadeId entidadeId, TVinculoId[] vinculosId);

        Task RemoverTodosVinculos(TEntidadeId entidadeId);

        Task AtualizarVinculos(TEntidadeId entidadeId, TVinculoId[] vinculosId);

        Task<IPagedList<TEntidade>> ListarTodosVinculosEntidade(TEntidadeId entidadeId, string[] includes = default, IPagedParam pagedParam = default);

        Task<IEnumerable<TVinculoId>> ListarTodosVinculos(TVinculoId[] vinculosId);
    }
}
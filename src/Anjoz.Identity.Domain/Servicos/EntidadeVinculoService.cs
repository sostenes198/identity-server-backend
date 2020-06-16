using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Repositorios.Crud;
using Anjoz.Identity.Domain.Contratos.Servicos.Crud;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Contratos.Validadores;
using Anjoz.Identity.Domain.Excecoes;
using Anjoz.Identity.Domain.Recursos;
using Anjoz.Identity.Domain.Servicos.Crud;

namespace Anjoz.Identity.Domain.Servicos
{
    public abstract class EntidadeVinculoService<TEntidade, TEntidadeId, TVinculoEntidade, TVinculoId> : CrudService<TEntidade, TEntidadeId>,
        IEntidadeVinculoService<TEntidade, TEntidadeId, TVinculoEntidade, TVinculoId>
        where TEntidade : class
        where TVinculoEntidade : class
    {
        protected readonly ICrudRepository<TEntidade, TEntidadeId> Repositorio;
        protected readonly ICrudService<TVinculoEntidade, TVinculoId> _vinculoService;

        protected EntidadeVinculoService(ICrudRepository<TEntidade, TEntidadeId> repositorio,
            ICrudService<TVinculoEntidade, TVinculoId> vinculoService, IDomainServiceValidator<TEntidade> validador)
            : base(repositorio, validador)
        {
            Repositorio = repositorio;
            _vinculoService = vinculoService;
        }

        public async Task AdicionarVinculo(TEntidadeId entidadeId, TVinculoId[] vinculosId)
        {
            if (!vinculosId.Any())
                return;

            await ExisteVinculos(vinculosId);
            var entidadeVinculos = PopularVinculos(entidadeId, vinculosId);

            await Repositorio.CriarAsync(entidadeVinculos);
        }

        public async Task RemoverTodosVinculos(TEntidadeId entidadeId)
        {
            var vinculosEntidadeParaRemover = (await ListarTodosVinculosEntidade(entidadeId)).ToList();
            if (vinculosEntidadeParaRemover.Any())
                await Repositorio.ExcluirAsync(vinculosEntidadeParaRemover);
        }

        public async Task AtualizarVinculos(TEntidadeId entidadeId, TVinculoId[] vinculosId)
        {
            await RemoverTodosVinculos(entidadeId);
            await AdicionarVinculo(entidadeId, vinculosId);
        }

        private async Task ExisteVinculos(TVinculoId[] vinculosId)
        {
            bool NaoExisteTodosVinculos(TVinculoId[] claims) => claims.Any();

            var vinculosIdAplicacao = (await ListarTodosVinculos(vinculosId));

            var excetoVinculos = vinculosId.Except(vinculosIdAplicacao).ToArray();

            if (NaoExisteTodosVinculos(excetoVinculos))
                throw new BusinessException(string.Format(Mensagens.Vinculos_NaoEncontrados, string.Join(",", excetoVinculos)));
        }

        public abstract ICollection<TEntidade> PopularVinculos(TEntidadeId entidadeId, TVinculoId[] vinculosId);

        public abstract Task<IPagedList<TEntidade>> ListarTodosVinculosEntidade(TEntidadeId entidadeId, string[] includes = default, IPagedParam pagedParam = default);

        public abstract Task<IEnumerable<TVinculoId>> ListarTodosVinculos(TVinculoId[] vinculosId);
    }
}
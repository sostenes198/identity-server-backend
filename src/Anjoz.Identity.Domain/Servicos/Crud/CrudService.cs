using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Repositorios.Crud;
using Anjoz.Identity.Domain.Contratos.Servicos.Crud;
using Anjoz.Identity.Domain.Contratos.Validadores;

namespace Anjoz.Identity.Domain.Servicos.Crud
{
    public class CrudService<T, TId> : ICrudService<T, TId>
        where T : class
    {
        private readonly ICrudRepository<T, TId> _repositorio;
        private readonly IDomainServiceValidator<T> _validador;

        public CrudService(ICrudRepository<T, TId> repositorio, IDomainServiceValidator<T> validador)
        {
            _repositorio = repositorio;
            _validador = validador;
        }

        public Task<T> ObterPorIdAsync(TId id) => AoObterPorIdAsync(id);

        public Task<IPagedList<T>> ListarPorAsync(Expression<Func<T, bool>> where = default, string[] includes = default, IPagedParam pagedParam = default)
            => AoListarTodosAsync(where, includes, pagedParam);

        public Task CriarAsync(T entidade) => AoCriarAsync(entidade);

        public Task AtualizarAsync(T entidade) => AoAtualizarAsync(entidade);

        public Task ExcluirAsync(TId id) => AoExcluirAsync(id);

        protected virtual Task<T> AoObterPorIdAsync(TId id) => _repositorio.ObterPorIdAsync(id);

        protected virtual Task<IPagedList<T>> AoListarTodosAsync(Expression<Func<T, bool>> where = default, string[] includes = default, IPagedParam pagedParam = default)
            => _repositorio.ListarPorAsync(where, includes, pagedParam);

        protected virtual async Task<T> AoCriarAsync(T entity)
        {
            _validador.Salvar?.ValidarEntidade(entity);
            await _repositorio.CriarAsync(entity);
            return entity;
        }

        protected virtual async Task<T> AoAtualizarAsync(T entity)
        {
            _validador.Atualizar?.ValidarEntidade(entity);
            await _repositorio.AtualizarAsync(entity);
            return entity;
        }

        protected virtual async Task<TId> AoExcluirAsync(TId id)
        {
            var entity = await ObterPorIdAsync(id);
            _validador.Deletar?.ValidarEntidade(entity);
            await _repositorio.ExcluirAsync(entity);
            return id;
        }
    }
}
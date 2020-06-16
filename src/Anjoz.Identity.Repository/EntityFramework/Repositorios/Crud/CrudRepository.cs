using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Repositorios.Crud;
using Anjoz.Identity.Domain.Entidades.Paginacao;
using Anjoz.Identity.Infrastructure.Extensoes;
using Anjoz.Identity.Repository.EntityFramework.Context;
using Anjoz.Identity.Repository.EntityFramework.Extensoes;
using Microsoft.EntityFrameworkCore;

namespace Anjoz.Identity.Repository.EntityFramework.Repositorios.Crud
{
    public class CrudRepository<T, TId> : ICrudRepository<T, TId>
        where T : class
    {
        private readonly IdentityContext _context;

        public CrudRepository(IdentityContext context)
        {
            _context = context;
        }

        public async Task<T> ObterPorIdAsync(TId id)
        {
            var resultado = await AoObterPorIdAsync(id);
            return RetornarResultadoDetachado(resultado);
        }

        public Task<IPagedList<T>> ListarPorAsync(Expression<Func<T, bool>> where = default, string[] includes = default, IPagedParam pagedParam = default)
        {
            where ??= PredicateBuilderExtension.True<T>();
            includes ??= new string[0];
            pagedParam ??= new PagedParam();
            return AoListarPorAsync(where, includes, pagedParam);
        }

        public async Task CriarAsync(T entidade)
        {
            await AoCriarAsync(entidade);
            DetacharResultado(entidade);
        }

        public async Task CriarAsync(ICollection<T> entidades)
        {
            await AoCriarAsync(entidades);

            foreach (var entidade in entidades)
                DetacharResultado(entidade);
        }

        public async Task AtualizarAsync(T entidade)
        {
            _context.Set<T>().Attach(entidade);
            await AoAtualizarAsync(entidade);
            DetacharResultado(entidade);
        }

        public Task ExcluirAsync(T entidade) => AoExcluirAsync(entidade);
        public Task ExcluirAsync(ICollection<T> entidades) => AoExcluirAsync(entidades);

        protected virtual Task<T> AoObterPorIdAsync(TId id)
        {
            var dbSet = _context.Set<T>();

            if (id.GetType().IsPrimitive || id is string)
                return dbSet.FindAsync(id);

            var keyValues = id.GetType().GetProperties().Select(lnq => lnq.GetValue(id)).ToArray();
            return dbSet.FindAsync(keyValues);
        }

        protected virtual async Task<IPagedList<T>> AoListarPorAsync(Expression<Func<T, bool>> where, string[] includes, IPagedParam pagedParam)
        {
            var result = await _context.Set<T>()
                .PopularIncludes(includes)
                .Where(where)
                .Paginar(pagedParam)
                .ToListAsync();
            
          return new PagedList<T>(result, pagedParam.PageNumber, pagedParam.PageSize, pagedParam.TotalPages);
        }
            

        protected virtual async Task AoCriarAsync(T entidade)
        {
            await _context.AddAsync(entidade);
            await _context.SaveChangesAsync();
        }

        protected virtual async Task AoCriarAsync(ICollection<T> entidades)
        {
            await _context.AddRangeAsync(entidades);
            await _context.SaveChangesAsync();
        }

        protected virtual async Task AoAtualizarAsync(T entity)
        {
            var entityApplication = await _context.EncontrarEntidadeAsync(entity);

            _context.Entry(entityApplication).CurrentValues.SetValues(entity);
            _context.Entry(entityApplication).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            _context.Entry(entityApplication).State = EntityState.Detached;
        }

        protected virtual async Task AoExcluirAsync(T entidade)
        {
            _context.Remove(entidade);
            await _context.SaveChangesAsync();
        }

        protected virtual async Task AoExcluirAsync(ICollection<T> entidades)
        {
            _context.RemoveRange(entidades);
            await _context.SaveChangesAsync();
        }

        protected TResultado RetornarResultadoDetachado<TResultado>(TResultado resultado)
        {
            if (resultado != null)
                DetacharResultado(resultado);

            return resultado;
        }

        protected void DetacharResultado<TResultado>(TResultado resultado)
        {
            _context.Entry(resultado).State = EntityState.Detached;
        }
    }
}
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Repositorios.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Entidades.Paginacao;
using Anjoz.Identity.Infrastructure.Extensoes;
using Anjoz.Identity.Repository.EntityFramework.Context;
using Anjoz.Identity.Repository.EntityFramework.Extensoes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Anjoz.Identity.Repository.EntityFramework.Repositorios.Identity
{
    public class PerfilRepository : IPerfilRepository
    {
        private readonly RoleManager<Perfil> _perfilManager;
        private readonly IdentityContext _context;

        public PerfilRepository(RoleManager<Perfil> perfilManager, IdentityContext context)
        {
            _perfilManager = perfilManager;
            _context = context;
        }

        public async Task<Perfil> ObterPorNomeAsync(string nome, string[] includes = default)
        {
            includes ??= new string[0];

            var resultado = await _perfilManager.Roles.PopularIncludes(includes).FirstOrDefaultAsync(
                lnq => lnq.NormalizedName == nome.ToUpperInvariant()
            );
            return RetornarResultadoDetachado(resultado);
        }

        public async Task<Perfil> ObterPorIdAsync(int id)
        {
            var resultado = await _perfilManager.Roles.FirstOrDefaultAsync(
                lnq => lnq.Id == id
            );
            return RetornarResultadoDetachado(resultado);
        }

        public async Task<IPagedList<Perfil>> ListarPorAsync(Expression<Func<Perfil, bool>> where = default, string[] includes = default, IPagedParam pagedParam = default)
        {
            where ??= PredicateBuilderExtension.True<Perfil>();
            includes ??= new string[0];
            pagedParam ??= new PagedParam();

            var result = await _perfilManager.Roles.PopularIncludes(includes).AsNoTracking().Where(where).Paginar(pagedParam).ToListAsync();

            return new PagedList<Perfil>(result, pagedParam);
        }

        public async Task<IdentityResult> CriarAsync(Perfil perfil)
        {
            var resultado = await _perfilManager.CreateAsync(perfil);
            if (resultado.Succeeded)
                DetacharResultado(perfil);
            return resultado;
        }

        public async Task<IdentityResult> AtualizarAsync(Perfil perfil)
        {
            _context.Set<Perfil>().Attach(perfil);
            var resultado = await _perfilManager.UpdateAsync(perfil);
            if (resultado.Succeeded)
                DetacharResultado(perfil);

            return resultado;
        }

        public Task<IdentityResult> ExcluirAsync(Perfil perfil)
        {
            return _perfilManager.DeleteAsync(perfil);
        }

        private TResultado RetornarResultadoDetachado<TResultado>(TResultado resultado)
        {
            if (resultado != null)
                DetacharResultado(resultado);

            return resultado;
        }

        private void DetacharResultado<TResultado>(TResultado resultado)
        {
            _context.Entry(resultado).State = EntityState.Detached;
        }
    }
}
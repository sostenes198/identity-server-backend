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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IdentityContext _context;

        public UsuarioRepository(UserManager<Usuario> userManager, IdentityContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        public async Task<Usuario> ObterPorLoginAsync(string login, string[] includes = default)
        {
            includes ??= new string[0];

            var resultado = await _userManager.Users.PopularIncludes(includes).FirstOrDefaultAsync(
                lnq => lnq.LoginNormalizado == login.ToUpperInvariant()
            );
            return RetornarResultadoDetachado(resultado);
        }

        public async Task<IdentityResult> AtualizarSenha(Usuario usuario, string novaSenha)
        {
            _context.Set<Usuario>().Attach(usuario);
            var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
            return await _userManager.ResetPasswordAsync(usuario, token, novaSenha);
        }

        public async Task<Usuario> ObterPorNomeAsync(string nome, string[] includes = default)
        {
            includes ??= new string[0];

            var resultado = await _userManager.Users.PopularIncludes(includes).FirstOrDefaultAsync(
                lnq => lnq.NormalizedUserName == nome.ToUpperInvariant()
            );
            return RetornarResultadoDetachado(resultado);
        }

        public async Task<Usuario> ObterPorIdAsync(int id)
        {
            var resultado = await _userManager.Users.FirstOrDefaultAsync(
                lnq => lnq.Id == id
            );
            return RetornarResultadoDetachado(resultado);
        }

        public async Task<IPagedList<Usuario>> ListarPorAsync(Expression<Func<Usuario, bool>> where = default, string[] includes = default, IPagedParam pagedParam = default)
        {
            where ??= PredicateBuilderExtension.True<Usuario>();
            includes ??= new string[0];
            pagedParam ??= new PagedParam();

            var result = await _userManager.Users.PopularIncludes(includes).AsNoTracking().Where(where).Paginar(pagedParam).ToListAsync();

            return new PagedList<Usuario>(result, pagedParam);
        }

        public async Task<IdentityResult> CriarAsync(Usuario usuario, string password)
        {
            var resultado = await _userManager.CreateAsync(usuario, password);
            if (resultado.Succeeded)
                DetacharResultado(usuario);
            return resultado;
        }

        public async Task<IdentityResult> AtualizarAsync(Usuario usuario)
        {
            _context.Set<Usuario>().Attach(usuario);
            var resultado = await _userManager.UpdateAsync(usuario);
            if (resultado.Succeeded)
                DetacharResultado(usuario);
            return resultado;
        }

        public Task<IdentityResult> ExcluirAsync(Usuario usuario)
        {
            return _userManager.DeleteAsync(usuario);
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
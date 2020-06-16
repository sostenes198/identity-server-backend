using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Servicos.Crud;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.VO;

namespace Anjoz.Identity.Domain.Contratos.Servicos.Identity
{
    public interface IUsuarioService : ICrudService<Usuario, int>
    {
        Task<Usuario> ObterPorLoginAsync(string login, string[] includes = default);
        
        Task<Usuario> ObterPorNomeAsync(string nome, string[] includes = default);

        Task<IPagedList<Claim>> ListarClaims(int idUsuario, IPagedParam pagedParam = default);
        
        Task AlterarSenha(UsuarioAlteracaoSenhaVo usuarioAlteracaoSenha);
    }
}
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Servicos.Crud;
using Anjoz.Identity.Domain.Entidades.Identity;

namespace Anjoz.Identity.Domain.Contratos.Servicos.Identity
{
    public interface IPerfilService : ICrudService<Perfil, int>
    {
        Task<Perfil> ObterPorNomeAsync(string nome, string[] includes = default);

        Task<IPagedList<Claim>> ListarClaims(int idPerfil, IPagedParam pagedParam = default);
    }
}
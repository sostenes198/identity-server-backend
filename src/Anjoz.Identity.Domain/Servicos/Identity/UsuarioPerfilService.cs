using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Repositorios.Identity;
using Anjoz.Identity.Domain.Contratos.Servicos.Crud;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Contratos.Validadores;
using Anjoz.Identity.Domain.Entidades.Identity;

namespace Anjoz.Identity.Domain.Servicos.Identity
{
    public class UsuarioPerfilService : EntidadeVinculoService<UsuarioPerfil, int, Perfil, int>, IUsuarioPerfilService
    {
        public UsuarioPerfilService(IUsuarioPerfilRepository repositorio,
            ICrudService<Perfil, int> vinculoService, IDomainServiceValidator<UsuarioPerfil> validador) : base(repositorio, vinculoService, validador)
        {
        }

        public override ICollection<UsuarioPerfil> PopularVinculos(int entidadeId, int[] vinculosId)
        {
            ICollection<UsuarioPerfil> usuarioPerfis = new List<UsuarioPerfil>();

            foreach (var vinculoId in vinculosId)
                usuarioPerfis.Add(new UsuarioPerfil {UserId = entidadeId, RoleId = vinculoId});

            return usuarioPerfis;
        }

        public override Task<IPagedList<UsuarioPerfil>> ListarTodosVinculosEntidade(int entidadeId, string[] includes = default, IPagedParam pagedParam = default)
        {
            return Repositorio.ListarPorAsync(lnq => lnq.UserId == entidadeId, includes, pagedParam);
        }

        public override async Task<IEnumerable<int>> ListarTodosVinculos(int[] vinculosId)
        {
            return (await _vinculoService.ListarPorAsync(lnq => vinculosId.Contains(lnq.Id))).Select(lnq => lnq.Id);
        }
    }
}
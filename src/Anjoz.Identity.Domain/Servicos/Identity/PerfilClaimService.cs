using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Repositorios.Identity;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Contratos.Validadores;
using Anjoz.Identity.Domain.Entidades.Identity;

namespace Anjoz.Identity.Domain.Servicos.Identity
{
    public class PerfilClaimService : EntidadeVinculoService<PerfilClaim, int, Claim, int>, IPerfilClaimService
    {
        public PerfilClaimService(IPerfilClaimRepository repositorio,
            IClaimService vinculoService,
            IDomainServiceValidator<PerfilClaim> validador)
            : base(repositorio, vinculoService, validador)
        {
        }

        public override ICollection<PerfilClaim> PopularVinculos(int entidadeId, int[] vinculosId)
        {
            var perfilClaim = new List<PerfilClaim>();

            foreach (var vinculoId in vinculosId)
                perfilClaim.Add(new PerfilClaim {RoleId = entidadeId, ClaimId = vinculoId});

            return perfilClaim;
        }

        public override Task<IPagedList<PerfilClaim>> ListarTodosVinculosEntidade(int entidadeId, string[] includes = default, IPagedParam pagedParam = default)
        {
            return Repositorio.ListarPorAsync(lnq => lnq.RoleId == entidadeId, includes, pagedParam);
        }

        public override async Task<IEnumerable<int>> ListarTodosVinculos(int[] vinculosId)
        {
            return (await _vinculoService.ListarPorAsync(lnq => vinculosId.Contains(lnq.Id))).Select(lnq => lnq.Id);
        }
    }
}
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
    public class UsuarioClaimService : EntidadeVinculoService<UsuarioClaim, int, Claim, int>, IUsuarioClaimService
    {
        public UsuarioClaimService(
            IUsuarioClaimRepository repositorio,
            IClaimService vinculoService,
            IDomainServiceValidator<UsuarioClaim> validador) 
            : base(repositorio, vinculoService, validador)
        {
        }

        public override ICollection<UsuarioClaim> PopularVinculos(int entidadeId, int[] claimsId)
        {
            ICollection<UsuarioClaim> usuariosClaims = new List<UsuarioClaim>();

            foreach (var claimId in claimsId)
                usuariosClaims.Add(new UsuarioClaim {UserId = entidadeId, ClaimId = claimId});

            return usuariosClaims;
        }

        public override Task<IPagedList<UsuarioClaim>> ListarTodosVinculosEntidade(int entidadeId, string[] includes = default, IPagedParam pagedParam = default)
        {
            return Repositorio.ListarPorAsync(lnq => lnq.UserId == entidadeId, includes, pagedParam);
        }

        public override async Task<IEnumerable<int>> ListarTodosVinculos(int[] vinculosId)
        {
            return (await _vinculoService.ListarPorAsync(lnq => vinculosId.Contains(lnq.Id))).Select(lnq => lnq.Id);
        }
    }
}
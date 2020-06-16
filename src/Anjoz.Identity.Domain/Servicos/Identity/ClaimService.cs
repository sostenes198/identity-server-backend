using System.Threading.Tasks;
using Anjoz.Identity.Domain.Contratos.Repositorios.Identity;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Contratos.Validadores;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Servicos.Crud;

namespace Anjoz.Identity.Domain.Servicos.Identity
{
    public class ClaimService : CrudService<Claim, int>, IClaimService
    {
        public ClaimService(IClaimRepository claimRepository, IDomainServiceValidator<Claim> validador) 
            : base(claimRepository, validador)
        {
        }

        protected override Task<Claim> AoCriarAsync(Claim entity)
        {
            entity.Valor = entity.Valor.ToUpperInvariant();
            return base.AoCriarAsync(entity);
        }
    }
}
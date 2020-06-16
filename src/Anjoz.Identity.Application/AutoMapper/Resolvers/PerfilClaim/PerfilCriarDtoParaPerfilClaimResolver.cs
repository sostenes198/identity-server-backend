using System.Collections.Generic;
using Anjoz.Identity.Application.Dtos.Identity.Perfil;
using Anjoz.Identity.Domain.Entidades.Identity;
using AutoMapper;

namespace Anjoz.Identity.Application.AutoMapper.Resolvers.PerfilClaim
{
    public class PerfilCriarDtoParaPerfilClaimResolver : IValueResolver<PerfilCriarDto, Perfil, IEnumerable<Domain.Entidades.Identity.PerfilClaim>>
    {
        public IEnumerable<Domain.Entidades.Identity.PerfilClaim> Resolve(PerfilCriarDto source, Perfil destination, IEnumerable<Domain.Entidades.Identity.PerfilClaim> destMember, ResolutionContext context)
        {
            ICollection<Domain.Entidades.Identity.PerfilClaim> perfilClaims = new List<Domain.Entidades.Identity.PerfilClaim>();

            foreach (var claimID in source.ClaimsId)
                perfilClaims.Add(new Domain.Entidades.Identity.PerfilClaim {ClaimId = claimID});

            return perfilClaims;
        }
    }
}
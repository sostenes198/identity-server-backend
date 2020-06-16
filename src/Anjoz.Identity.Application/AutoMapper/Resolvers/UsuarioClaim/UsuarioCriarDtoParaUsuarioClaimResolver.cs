using System.Collections.Generic;
using Anjoz.Identity.Application.Dtos.Identity.Usuario;
using Anjoz.Identity.Domain.Entidades.Identity;
using AutoMapper;

namespace Anjoz.Identity.Application.AutoMapper.Resolvers.UsuarioClaim
{
    public class UsuarioCriarDtoParaUsuarioClaimResolver : IValueResolver<UsuarioCriarDto, Usuario, IEnumerable<Domain.Entidades.Identity.UsuarioClaim>>
    {
        public IEnumerable<Domain.Entidades.Identity.UsuarioClaim> Resolve(UsuarioCriarDto source, Usuario destination, IEnumerable<Domain.Entidades.Identity.UsuarioClaim> destMember, ResolutionContext context)
        {
            ICollection<Domain.Entidades.Identity.UsuarioClaim> usuarioClaims = new List<Domain.Entidades.Identity.UsuarioClaim>();
            foreach (var claimID in source.ClaimsId)
                usuarioClaims.Add(new Domain.Entidades.Identity.UsuarioClaim {ClaimId = claimID});

            return usuarioClaims;
        }
    }
}
using Anjoz.Identity.Application.AutoMapper.Profiles.Base;
using Anjoz.Identity.Application.Dtos.Identity.Claim;
using Anjoz.Identity.Domain.Entidades.Identity;

namespace Anjoz.Identity.Application.AutoMapper.Profiles.Identity
{
    public class ClaimProfile : Profile
    {
        public ClaimProfile()
        {
            CriarMapeamentoClaimDtoParaClaim();
            CriarMapeamentoClaimAtualizarDtoParaClaim();
            CriarMapeamentoClaimCriarDtoParaClaim();
        }

        private void CriarMapeamentoClaimDtoParaClaim()
        {
            CreateMap<Claim, ClaimDto>();
        }

        private void CriarMapeamentoClaimAtualizarDtoParaClaim()
        {
            CreateMap<ClaimAtualizarDto, Claim>()
                .ForMember(lnq => lnq.ValorNormalizado, opt => opt.Ignore())
                .ForMember(lnq => lnq.DescricaNormalizada, opt => opt.Ignore())
                .ForMember(lnq => lnq.PerfisClaims, opt => opt.Ignore())
                .ForMember(lnq => lnq.UsuariosClaims, opt => opt.Ignore());
        }

        private void CriarMapeamentoClaimCriarDtoParaClaim()
        {
            CreateMap<ClaimCriarDto, Claim>()
                .ForMember(lnq => lnq.Id, opt => opt.Ignore())
                .ForMember(lnq => lnq.ValorNormalizado, opt => opt.Ignore())
                .ForMember(lnq => lnq.DescricaNormalizada, opt => opt.Ignore())
                .ForMember(lnq => lnq.PerfisClaims, opt => opt.Ignore())
                .ForMember(lnq => lnq.UsuariosClaims, opt => opt.Ignore());
        }
    }
}
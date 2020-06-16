using Anjoz.Identity.Application.AutoMapper.Profiles.Base;
using Anjoz.Identity.Application.AutoMapper.Resolvers.PerfilClaim;
using Anjoz.Identity.Application.Dtos.Identity.Perfil;
using Anjoz.Identity.Domain.Entidades.Identity;

namespace Anjoz.Identity.Application.AutoMapper.Profiles.Identity
{
    public class PerfilProfile : Profile
    {
        public PerfilProfile()
        {
            CreateMap<Perfil, PerfilDto>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Name));

            CreateMap<PerfilCriarDto, Perfil>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PerfisClaims, opt => opt.MapFrom<PerfilCriarDtoParaPerfilClaimResolver>())
                .ForMember(dest => dest.UsuarioPerfis, opt => opt.Ignore())
                .ForMember(dest => dest.NormalizedName, opt => opt.Ignore())
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore());

            CreateMap<PerfilAtualizarDto, Perfil>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.PerfisClaims, opt => opt.MapFrom<PerfilAtualizarDtoParaPerfilPerfilClaimResolver>())
                .ForMember(dest => dest.UsuarioPerfis, opt => opt.Ignore())
                .ForMember(dest => dest.NormalizedName, opt => opt.Ignore())
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore());
        }
    }
}
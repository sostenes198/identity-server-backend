using Anjoz.Identity.Application.AutoMapper.Profiles.Base;
using Anjoz.Identity.Application.Dtos.Login;
using Anjoz.Identity.Domain.Entidades.Login;

namespace Anjoz.Identity.Application.AutoMapper.Profiles.Login
{
    public class LoginUsuarioProfile : Profile
    {
        public LoginUsuarioProfile()
        {
            CreateMap<LoginUsuario, LoginUsuarioDto>()
                .ForMember(dest => dest.Nome,
                    opt => opt.MapFrom(src => src.Usuario.UserName))
                .ForMember(dest => dest.Login,
                    opt => opt.MapFrom(src => src.Usuario.Login))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Usuario.Email))
                .ForMember(dest => dest.AcessToken,
                    opt => opt.MapFrom(src => src.AcessToken))
                .ForMember(dest => dest.Claims,
                    opt => opt.MapFrom(src => src.Claims));
        }
    }
}
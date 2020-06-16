using Anjoz.Identity.Application.AutoMapper.Profiles.Base;
using Anjoz.Identity.Application.Dtos.Login;

namespace Anjoz.Identity.Application.AutoMapper.Profiles.Login
{
    public class LoginProfile : Profile
    {
        public LoginProfile()
        {
            CreateMap<LoginDto, Domain.Entidades.Login.Login>()
                .ForMember(dest => dest.LoginUsuario, opt => opt.MapFrom(src => src.Login));

            CreateMap<Domain.Entidades.Login.Login, LoginDto>()
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.LoginUsuario));
        }
    }
}
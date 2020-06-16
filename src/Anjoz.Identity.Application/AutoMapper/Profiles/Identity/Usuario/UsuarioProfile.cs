using Anjoz.Identity.Application.AutoMapper.Profiles.Base;
using Anjoz.Identity.Application.AutoMapper.Resolvers.UsuarioClaim;
using Anjoz.Identity.Application.AutoMapper.Resolvers.UsuarioPerfil;
using Anjoz.Identity.Application.Dtos.Identity.Usuario;

namespace Anjoz.Identity.Application.AutoMapper.Profiles.Identity.Usuario
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CriarMapeamentoUsuarioParaUsuarioDto();
            CriarMapeamentoUsuarioCriarDtoParaUsuario();
            CriarMapeamentoUsuarioAtualizarParaUsuario();
        }

        private void CriarMapeamentoUsuarioParaUsuarioDto()
        {
            CreateMap<Domain.Entidades.Identity.Usuario, UsuarioDto>()
                .ForMember(lnq => lnq.Nome, opt => opt.MapFrom(src => src.UserName))
                .ForMember(lnq => lnq.Telefone, opt => opt.MapFrom(src => src.PhoneNumber));
        }

        private void CriarMapeamentoUsuarioCriarDtoParaUsuario()
        {
            CreateMap<UsuarioCriarDto, Domain.Entidades.Identity.Usuario>()
                .ForMember(lnq => lnq.UserName, opt => opt.MapFrom(src => src.Nome))
                .ForMember(lnq => lnq.PhoneNumber, opt => opt.MapFrom(src => src.Telefone))
                .ForMember(lnq => lnq.PasswordHash, opt => opt.MapFrom(prop => prop.Senha))
                .ForMember(lnq => lnq.UsuariosClaims, opt => opt.MapFrom<UsuarioCriarDtoParaUsuarioClaimResolver>())
                .ForMember(lnq => lnq.UsuariosPerfis, opt => opt.MapFrom<UsuarioCriarDtoParaUsuarioPerfilResolver>())
                .ForMember(lnq => lnq.Id, opt => opt.Ignore())
                .ForMember(lnq => lnq.NormalizedUserName, opt => opt.Ignore())
                .ForMember(lnq => lnq.NormalizedEmail, opt => opt.Ignore())
                .ForMember(lnq => lnq.EmailConfirmed, opt => opt.Ignore())
                .ForMember(lnq => lnq.SecurityStamp, opt => opt.Ignore())
                .ForMember(lnq => lnq.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(lnq => lnq.PhoneNumberConfirmed, opt => opt.Ignore())
                .ForMember(lnq => lnq.TwoFactorEnabled, opt => opt.Ignore())
                .ForMember(lnq => lnq.LockoutEnd, opt => opt.Ignore())
                .ForMember(lnq => lnq.LockoutEnabled, opt => opt.Ignore())
                .ForMember(lnq => lnq.AccessFailedCount, opt => opt.Ignore())
                .ForMember(lnq => lnq.LoginNormalizado, opt => opt.Ignore());
        }

        private void CriarMapeamentoUsuarioAtualizarParaUsuario()
        {
            CreateMap<UsuarioAtualizarDto, Domain.Entidades.Identity.Usuario>()
                .ForMember(lnq => lnq.UserName, opt => opt.MapFrom(src => src.Nome))
                .ForMember(lnq => lnq.PhoneNumber, opt => opt.MapFrom(src => src.Telefone))
                .ForMember(lnq => lnq.UsuariosClaims, opt => opt.MapFrom<UsuarioAtualizarDtoParaUsuarioClaimResolver>())
                .ForMember(lnq => lnq.UsuariosPerfis, opt => opt.MapFrom<UsuarioAtualizarDtoParaUsuarioPerfilResolver>())
                .ForMember(lnq => lnq.PasswordHash, opt => opt.Ignore())
                .ForMember(lnq => lnq.NormalizedUserName, opt => opt.Ignore())
                .ForMember(lnq => lnq.NormalizedEmail, opt => opt.Ignore())
                .ForMember(lnq => lnq.EmailConfirmed, opt => opt.Ignore())
                .ForMember(lnq => lnq.SecurityStamp, opt => opt.Ignore())
                .ForMember(lnq => lnq.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(lnq => lnq.PhoneNumberConfirmed, opt => opt.Ignore())
                .ForMember(lnq => lnq.TwoFactorEnabled, opt => opt.Ignore())
                .ForMember(lnq => lnq.LockoutEnd, opt => opt.Ignore())
                .ForMember(lnq => lnq.LockoutEnabled, opt => opt.Ignore())
                .ForMember(lnq => lnq.AccessFailedCount, opt => opt.Ignore())
                .ForMember(lnq => lnq.LoginNormalizado, opt => opt.Ignore());
        }
    }
}
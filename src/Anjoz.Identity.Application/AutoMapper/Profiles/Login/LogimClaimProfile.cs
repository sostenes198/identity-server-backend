using Anjoz.Identity.Application.AutoMapper.Profiles.Base;
using Anjoz.Identity.Application.Dtos.Login;
using Anjoz.Identity.Domain.Entidades.Identity;

namespace Anjoz.Identity.Application.AutoMapper.Profiles.Login
{
    public class LogimClaimProfile : Profile
    {
        public LogimClaimProfile()
        {
            CreateMap<Claim, LoginClaimDto>();
        }
    }
}
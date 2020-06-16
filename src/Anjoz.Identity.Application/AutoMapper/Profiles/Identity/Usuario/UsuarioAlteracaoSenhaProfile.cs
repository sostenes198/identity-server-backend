using Anjoz.Identity.Application.AutoMapper.Profiles.Base;
using Anjoz.Identity.Application.Dtos.Identity.Usuario;
using Anjoz.Identity.Application.Dtos.Identity.Usuario.AlteracaoSenha;
using Anjoz.Identity.Domain.VO;

namespace Anjoz.Identity.Application.AutoMapper.Profiles.Identity.Usuario
{
    public class UsuarioAlteracaoSenhaProfile : Profile
    {
        public UsuarioAlteracaoSenhaProfile()
        {
            CreateMap<UsuarioAlteracaoSenhaDto, UsuarioAlteracaoSenhaVo>();
        }
    }
}
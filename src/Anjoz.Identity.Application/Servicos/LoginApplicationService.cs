using System.Threading.Tasks;
using Anjoz.Identity.Application.Contratos.Login;
using Anjoz.Identity.Application.Dtos.Login;
using Anjoz.Identity.Domain.Contratos.Servicos;
using Anjoz.Identity.Domain.Entidades.Login;
using AutoMapper;

namespace Anjoz.Identity.Application.Servicos
{
    public class LoginApplicationService : ILoginApplicationService
    {
        private readonly IMapper _mapper;
        private readonly ILoginService _service;

        public LoginApplicationService(IMapper mapper, ILoginService service)
        {
            _mapper = mapper;
            _service = service;
        }
        
        public async Task<LoginUsuarioDto> Login(LoginDto loginDto)
        {
            var login = _mapper.Map<LoginDto, Login>(loginDto);
            var loginusuario = await _service.Login(login);

            return _mapper.Map<LoginUsuario, LoginUsuarioDto>(loginusuario);
        }
    }
}
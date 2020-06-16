using System.Threading.Tasks;
using Anjoz.Identity.Application.Contratos.Login;
using Anjoz.Identity.Application.Dtos.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ControllerBase = Anjoz.Identity.WebApi.Controllers.Base.ControllerBase;

namespace Anjoz.Identity.WebApi.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly ILoginApplicationService _loginApplicationService;

        public LoginController(ILoginApplicationService loginApplicationService)
        {
            _loginApplicationService = loginApplicationService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoginUsuarioDto>> Login(LoginDto loginDto)
        {
            var loginUsuarioDto = await _loginApplicationService.Login(loginDto);

            return Ok(loginUsuarioDto);
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ControllerBase = Anjoz.Identity.WebApi.Controllers.Base.ControllerBase;

namespace Anjoz.Identity.WebApi.Controllers
{
    public class CheckController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Check()
        {
            return Ok("ONLINE");
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Anjoz.Identity.WebApi.Controllers.Base
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
    }
}
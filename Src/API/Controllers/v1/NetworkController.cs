using System.Threading.Tasks;
using Api.Controllers.Common;
using Api.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class NetworkController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet("test-connection")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> TestConnection()
        {
            await Task.Delay(0);
            return NoContent();
        }
    }
}
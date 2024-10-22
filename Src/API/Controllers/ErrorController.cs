using Api.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    // If api endpoint doesnt exist fall back to error controller
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)] // ignore from swagger documentation
    public class ErrorController : ControllerBase
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}

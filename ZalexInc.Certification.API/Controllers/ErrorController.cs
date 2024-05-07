using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ZalexInc.Certification.API.Controllers
{
    [Route("error/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(code);
        }
    }
}

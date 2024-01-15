using Microsoft.AspNetCore.Mvc;

namespace e_Estoque_API.API.Controllers
{

    [ApiController]
    public class MainController : ControllerBase
    {
        protected IActionResult CustomResponse(bool success, object? result = null)
        {
            if (success)
            {
                return Ok(result);

            }

            return BadRequest(result);
        }
    }
}

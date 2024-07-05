namespace HR.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PingController : ControllerBase
{
    [HttpGet]
    [Authorize]

    public IActionResult Ping()
    {
        return Ok("--> Ping Ok");
    }
}

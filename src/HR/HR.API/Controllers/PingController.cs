using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PingController : ControllerBase
{
    [HttpGet]
    public IActionResult Ping()
    {
        return Ok("--> Ping Ok");
    }
}

using System.Security.Claims;
namespace Shared.AspNetCore;

[Produces("application/json")]
public class BaseController : ControllerBase
{
    // to: do
    // Add Logger -> use microsoft logging library
    public BaseController()
    {
          // inject logger
    }

    protected static DateTime CurrentDateTime { 
        get
        { 
            return DateTime.UtcNow;
        }
    }

    protected Guid UserId
    {
        get
        {
            return Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
        }
    }

    protected IActionResult ReturnResponse(dynamic model)
    {
        if(model.Status == RequestExecution.Successful)
        {
            return Ok(model);
        }

        // log the error here

        return BadRequest();
    }

    protected IActionResult HandleError(Exception ex, string customMessage = null)
    {
        // log the stack trace and exception here

        BaseResponse<string> response = new()
        {
            StatusCode = (int)RequestExecution.Error
        };

#if DEBUG
        response.Errors = new List<string> { $"Error: {ex?.InnerException?.Message ?? ex.Message} stack trace: {ex?.StackTrace}" };
#else
        response.Errors = new List<string>() { "An error occured while processing your request..." };
#endif
        return BadRequest(response);
    }
}

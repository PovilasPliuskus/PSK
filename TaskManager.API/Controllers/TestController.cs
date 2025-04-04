using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api")]
[ApiController]
public class ProtectedController : ControllerBase
{
    [HttpGet("protected-endpoint")]
    [Authorize]
    public IActionResult GetProtectedData()
    {
        return Ok(new { message = "This is a protected resource!" });
    }

    [HttpGet("error")]
    [Authorize]
    public IActionResult GetError()
    {
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "This is a simulated error"
        };

        return StatusCode(problemDetails.Status.Value, problemDetails);
    }
}

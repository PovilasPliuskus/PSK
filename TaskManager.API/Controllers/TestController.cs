using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Exceptions;

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
        throw new SampleException("This is a simulated error");
    }
}

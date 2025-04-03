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
}

using Microsoft.AspNetCore.Mvc;

namespace AuthMicroservice.Controllers;

[Route("{controller}")]
public class TestController : ControllerBase
{
    [HttpGet]
    [Route("authTest")]
    public async Task<IActionResult> AuthTest([FromQuery] string message)
    {
        return await Task.Run(() => Ok("Auth test is OK!"));
    }
}

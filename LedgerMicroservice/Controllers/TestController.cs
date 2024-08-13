using Microsoft.AspNetCore.Mvc;

namespace LedgerMicroservice.Controllers;

[Route("{controller}")]
public class TestController : ControllerBase
{
    [HttpGet]
    [Route("getInfo")]
    public async Task<IActionResult> GetInfo() =>
         await Task.Run(() =>
         {
             var contentString = $"Controller type: [{nameof(TestController)}]. " +
                         $"Method: [{nameof(GetInfo)}]. Time:[{DateTime.Now}].";
             Console.WriteLine(contentString);
             return Ok(contentString);
         });

    [HttpPost]
    [Route("doAction")]
    public async Task<IActionResult> DoAction([FromBody] string message)
    {
        Console.WriteLine($"Controller type: [{nameof(TestController)}]. " +
                        $"Method: [{nameof(DoAction)}]. Time:[{DateTime.Now}]. Message: [{message}].");
        return Ok();
    }
}

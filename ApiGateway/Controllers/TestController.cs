using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[Route("{controller}")]
public class TestController(Salary.SalaryService.SalaryServiceClient salaryServiceGrpcClient) : ControllerBase
{
    [HttpGet]
    [Route("getInfo")]
    public async Task<IActionResult> GetInfo() 
    {
        var getInfoRequest = new Salary.GetInfoRequest 
            { Content = $"GetInfo request for Salary Microservice at [{DateTime.Now}]." };
        var getInfoResponse = await salaryServiceGrpcClient.GetInfoAsync(getInfoRequest);
        var contentString = $"Controller type: [{nameof(TestController)}]. " +
                    $"Method: [{nameof(GetInfo)}]. Time:[{DateTime.Now}]. " +
                    $"Response from GetInfo grpc call to Salary Service: [{getInfoResponse}]. " +
                    $"";
        Console.WriteLine(contentString);
        return Ok(contentString);
    }

    [HttpPost]
    [Route("doAction")]
    public async Task<IActionResult> DoAction([FromBody] string message)
    {
        var doActionRequest = new Salary.DoActionRequest
            { Content = $"DoAction request for Salary Microservice at [{DateTime.Now}]." };
        var doActionResponse = await salaryServiceGrpcClient.DoActionAsync(doActionRequest);
        var contentString = $"Controller type: [{nameof(TestController)}]. " +
                    $"Method: [{nameof(GetInfo)}]. Time:[{DateTime.Now}]. " +
                    $"Response from DoAction grpc call to Salary Service: [{doActionResponse}]. " +
                    $"";
        Console.WriteLine(contentString);
        return Ok(contentString);
    }
}

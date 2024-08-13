using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace ApiGateway.Controllers;

[Route("{controller}")]
public class TestController(
    Ledger.LedgerService.LedgerServiceClient ledgerServiceGrpcClient, 
    IHttpClientFactory httpClientFactory) : ControllerBase
{
    [HttpGet]
    [Route("getInfo")]
    public async Task<IActionResult> GetInfo() 
    {
        var getInfoRequest = new Ledger.GetInfoRequest 
            { Content = $"GetInfo request for Ledger Microservice at [{DateTime.Now}]." };
        var getInfoResponse = await ledgerServiceGrpcClient.GetInfoAsync(getInfoRequest);
        var contentString = $"Controller type: [{nameof(TestController)}]. " +
                    $"Method: [{nameof(GetInfo)}]. Time:[{DateTime.Now}]. " +
                    $"Response from GetInfo grpc call to Ledger Service: [{getInfoResponse}]. " +
                    $"";
        Console.WriteLine(contentString);
        return Ok(contentString);
    }

    [HttpPost]
    [Route("doAction")]
    public async Task<IActionResult> DoAction([FromBody] string message)
    {
        var doActionRequest = new Ledger.DoActionRequest
            { Content = $"DoAction request for Ledger Microservice at [{DateTime.Now}]." };
        var doActionResponse = await ledgerServiceGrpcClient.DoActionAsync(doActionRequest);
        var contentString = $"Controller type: [{nameof(TestController)}]. " +
                    $"Method: [{nameof(GetInfo)}]. Time:[{DateTime.Now}]. " +
                    $"Response from DoAction grpc call to Ledger Service: [{doActionResponse}]. " +
                    $"";
        Console.WriteLine(contentString);
        return Ok(contentString);
    }

    [HttpGet]
    [Route("authTest")]
    public async Task<IActionResult> AuthTest()
    {
        var client = httpClientFactory.CreateClient("AuthServiceClient");
        var content = "Hello to the Auth Service!";
        var encodedContent = HttpUtility.UrlEncode(content);
        var baseUrl = "test/authTest";
        var urlWithQuery = $"{baseUrl}?message={encodedContent}";
        var response = await client.GetAsync(urlWithQuery);
        var responseMessage = await response.Content.ReadAsStringAsync();
        var authTestName = nameof(AuthTest);
        var contentString = $"Controller type: [{nameof(TestController)}]. " +
                    $"Method: [{authTestName}]. Time:[{DateTime.Now}]. " +
                    $"Response from {authTestName} http call to Auth Service: [{responseMessage}]. " +
                    $"";
        Console.WriteLine(contentString);
        return Ok(contentString);
    }
}

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddHttpClient("GatewayClient", client => client.BaseAddress = new Uri("https://host.docker.internal:32768/"))
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    });
var app = builder.Build();
app.UseHttpsRedirection();

app.MapGet("/initiate", async (HttpContext context) =>
{
    var serviceProvider = app.Services;
    var factory = serviceProvider.GetRequiredService<IHttpClientFactory>();
    var client = factory.CreateClient("GatewayClient");
    var content = "Hello to the Gateway!";
    var stringContent = JsonContent.Create(content);

    var responseToDoAction = await client.PostAsync("test/doAction", stringContent);
    var doActionResponseMessage = $"Response to Do Action: [{responseToDoAction}].";
    Console.WriteLine(doActionResponseMessage);

    var responseToGetInfo = await client.GetAsync("test/getInfo");
    var getInfoResponseMessage = $"Response to GetInfo: [{responseToGetInfo}].";
    Console.WriteLine(getInfoResponseMessage);

    var responseToAuthTest = await client.GetAsync("test/authTest");
    var authTestResponseMessage = $"Response to AuthTest: [{responseToAuthTest}].";
    Console.WriteLine(authTestResponseMessage);

    return Results.Json(new { message = $"{getInfoResponseMessage} {doActionResponseMessage} {authTestResponseMessage}" });
});

app.Run();
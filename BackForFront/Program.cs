var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddHttpClient("GatewayClient", client => client.BaseAddress = new Uri("https://host.docker.internal:7242/"))
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
    var responseToPost = await client.PostAsync("test/doAction", stringContent);
    var responseToGet = await client.GetAsync("test/getInfo");
    var getResponseMessage = $"Response to GetInfo: [{responseToGet}].";
    var postResponseMessage = $"Response to Do Action: [{responseToPost}].";
    Console.WriteLine(getResponseMessage);
    Console.WriteLine(postResponseMessage);
    return $"{getResponseMessage} {postResponseMessage}";
});

app.Run();
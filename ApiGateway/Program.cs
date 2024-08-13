var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("ApiGateway started.");

var services = builder.Services;
services.AddControllers();
services.AddGrpcClient<Ledger.LedgerService.LedgerServiceClient>(
    o => { o.Address = new Uri("https://host.docker.internal:32798"); })
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        return new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
    });
services.AddHttpClient("AuthServiceClient", client => client.BaseAddress = new Uri("https://host.docker.internal:32790/"))
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
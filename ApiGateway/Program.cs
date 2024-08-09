var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("ApiGateway started.");

var services = builder.Services;
services.AddControllers();
services.AddGrpcClient<Salary.SalaryService.SalaryServiceClient>(
    o => { o.Address = new Uri("https://host.docker.internal:32772"); })
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        return new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
    });
services.AddHttpClient("AuthServiceClient", client => client.BaseAddress = new Uri("https://host.docker.internal:32770/"))
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
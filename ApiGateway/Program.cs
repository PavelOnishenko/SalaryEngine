var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllers();
services.AddGrpcClient<Salary.SalaryService.SalaryServiceClient>(
    o => { o.Address = new Uri("https://host.docker.internal:32768"); })
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        return new HttpClientHandler
        {
            // WARNING: Only use in development environments
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
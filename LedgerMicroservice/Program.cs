using LedgerMicroservice.GrpcServices;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddGrpc();
services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGrpcService<LedgerService>();

app.Run();

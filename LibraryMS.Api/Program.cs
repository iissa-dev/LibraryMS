using LibraryMS.Api.Exceptions;
using LibraryMS.Application.DependencyInjection;
using LibraryMS.Infrastructure.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
// Serilog Config
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
// Add Exceptions
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
// Infrastructure Services
builder.Services.AddInfrastructure(builder.Configuration);
// Application Services
builder.Services.AddApplication();

var app = builder.Build();

app.UseExceptionHandler();
// Migrations
await app.Services.InitializeDatabaseAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
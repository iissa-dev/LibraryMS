using LibraryMS.Api.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddPresentation();
}
// Serilog Config
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

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
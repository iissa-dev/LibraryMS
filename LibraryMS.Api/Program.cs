using LibraryMS.Api.DependencyInjection;
using LibraryMS.Api.Hubs;

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
app.UseStaticFiles();

app.UseCors("ReactAppPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>("/hubs/notifications");

app.MapFallbackToFile("index.html");
await app.RunAsync();
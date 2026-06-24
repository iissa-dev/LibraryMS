using LibraryMS.Api.Services;
using LibraryMS.Application.Common.Interfaces;

namespace LibraryMS.Api.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddProblemDetails();
        services.AddSwaggerGen();
        services.AddOpenApi();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddScoped<IAuthorizationHandler, EntityAccessHandler>();

        services.AddScoped<INotificationService, SignalRNotificationService>();
        services.AddSignalR();

        services.AddCors(options =>
        {
            options.AddPolicy("ReactAppPolicy", policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
            });
        });

        return services;
    }
}
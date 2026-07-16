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
                policy.WithOrigins("http://localhost:5173", "http://192.168.100.45:5173", "https://barstool-grower-churn.ngrok-free.dev")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
            });
        });

        return services;
    }
}
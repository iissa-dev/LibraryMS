using LibraryMS.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryMS.Application.DependencyInjection;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options => { options.RegisterServicesFromAssembly(typeof(IAssemblyMarker).Assembly); });

        services.AddValidatorsFromAssembly(typeof(IAssemblyMarker).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using LibraryMS.Infrastructure.Interceptors;
using LibraryMS.Infrastructure.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace LibraryMS.Infrastructure.DependencyInjection;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            .AddInterceptors(new SoftDeleteInterceptor());
        });

        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
           {
               options.Password.RequireDigit = true;
               options.Password.RequireLowercase = true;
               options.Password.RequireNonAlphanumeric = true;
               options.Password.RequireUppercase = true;
               options.Password.RequiredLength = 6;
           })
           .AddEntityFrameworkStores<AppDbContext>()
           .AddDefaultTokenProviders();


        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwtOptions =>
            {
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidIssuer = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!)
                    )
                };

            });

        services.AddAuthorization();
        services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();
        services.AddScoped<IIdentityUser, Identity.IdentityUser>();
        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<ICodeGeneratorService, CodeGeneratorService>();


        services.AddHostedService<ReservationCheckJob>();

        return services;
    }

    public static async Task InitializeDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var serviceProvider = scope.ServiceProvider;

        try
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            await context.Database.MigrateAsync();
            await context.SeedAsync(roleManager);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
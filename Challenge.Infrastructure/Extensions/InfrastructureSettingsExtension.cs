using Challenge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Challenge.Infrastructure.Extensions;

public static class InfrastructureSettingsExtension
{
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<ApplicationDbContext>(options =>
             options.UseNpgsql(configuration.GetConnectionString("ApplicationDbContext"))
             .EnableSensitiveDataLogging(bool.Parse(configuration["DbContextOptions:SensitiveDataLoggingEnabled"] ?? "false"))
             .EnableDetailedErrors(bool.Parse(configuration["DbContextOptions:DetailedErrorsEnabled"] ?? "false")));

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services;
    }
}

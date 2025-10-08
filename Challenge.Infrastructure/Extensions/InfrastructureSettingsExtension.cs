using Challenge.Domain.Interfaces.Repositories;
using Challenge.Domain.Interfaces.Repositories.Base;
using Challenge.Infrastructure.Data;
using Challenge.Infrastructure.Repositories;
using Challenge.Infrastructure.Repositories.Base;
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
        return services
        .AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>()
        .AddScoped<IUserRepository, UserRepository>()
        .AddScoped<IAuthTokenRepository, AuthTokenRepository>()
        .AddScoped<IClientRepository, ClientRepository>()
        .AddScoped<IProductRepository, ProductRepository>()
        .AddScoped<IOrderRepository, OrderRepository>()
        .AddScoped<IOrderItemRepository, OrderItemRepository>();
    }
}

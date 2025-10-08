using Challenge.Application.Interfaces.Services;
using Challenge.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Challenge.Application.Extensions;

public static class ApplicationSettingsExtension
{
    // private readonly 
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IClientService, ClientService>()
            .AddScoped<IAuthTokenService, AuthTokenService>()
            .AddScoped<IProductService, ProductService>()
            .AddScoped<IOrderService, OrderService>();

        return services;
    }

    public class LoggingHandler(ILogger<LoggingHandler> logger) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            logger.LogInformation("{Method} {RequestUri}", request.Method, request.RequestUri);

            if (request.Content != default)
            {
                logger.LogInformation("{Request}", await request.Content.ReadAsStringAsync(cancellationToken));
            }

            var response = await base.SendAsync(request, cancellationToken);

            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
            logger.LogInformation("{ResponseContent}", responseContent);

            return response;
        }
    }
}

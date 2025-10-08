using Challenge.Api.Extensions;
using Challenge.Api.Middlewares;
using Challenge.Infrastructure.Data;
using Challenge.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Challenge.Application.Extensions;
using System.Diagnostics.CodeAnalysis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .ConfigureDbContext(builder.Configuration)
    .RegisterServices(builder.Configuration)
    .AddJwtConfiguration(builder.Configuration)
    .AddRepositories()
    .AddApiSettings()
    .AddApiTools()
    .ConfigureCompression();

builder.Services.AddProblemDetails();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.WebHost
    .ConfigureKestrel(options => { options.AddServerHeader = false; })
    .UseIIS();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DbInitializer.Seed(context);
}

app.UseApiTools(app.Configuration);

app.UseMiddleware<JwtMiddleware>();

app.UseApiSettings()
    .UseResponseCompression()
    .UseExceptionHandler()
    .UseRouting();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

app.Run();

namespace CubosCard.API
{
    [ExcludeFromCodeCoverage]
    public partial class Program
    {
        protected Program() { }
    }
}
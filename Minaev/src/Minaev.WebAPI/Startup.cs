using EFConfigurator;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Minaev.Services.Configurator;

namespace Minaev.WebAPI;

public static class Startup
{
    public static void Initialize(IServiceCollection services, String environment, ConfigurationManager? configuration)
    {
        services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql("Server=localhost;Port=5432;Database=simbirgo;User Id=simbirgoadmin;Password=simbirgoadminpass;");
        });

        services.Initialize();
    }

    public static void Initialize(this WebApplicationBuilder builder, Action<IServiceCollection>? action = null)
    {
        Initialize(builder.Services, builder.Environment.EnvironmentName, builder.Configuration);

        if (action is not null)
            action(builder.Services);
    }

    public static IServiceCollection AddHttps(this IServiceCollection services)
    {
        services.AddHsts(options =>
        {
            options.Preload = true;
            options.IncludeSubDomains = true;
            options.MaxAge = TimeSpan.FromDays(60);
        });

        services.AddHttpsRedirection(options =>
        {
            options.HttpsPort = 443;
            options.RedirectStatusCode = StatusCodes.Status301MovedPermanently;
        });

        return services;
    }
    public static IServiceCollection AddResponseCompressionProviders(this IServiceCollection services, String[]? mimeTypes = null)
    {
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();

            if (mimeTypes is not null)
                options.MimeTypes = mimeTypes;
        });

        return services;
    }

    public static void UseHttps(this WebApplication app)
    {
        if (app.Environment.IsDevelopment()) return;

        app.UseHsts();
        app.UseRewriter(new RewriteOptions()
            .AddRedirectToNonWww()
            .AddRedirectToHttps()
        );
    }

    public static void UseCors(this WebApplication app)
    {
        app.UseCors(config => config
            .AllowAnyOrigin()
            .WithMethods("OPTIONS", "GET", "POST")
            .AllowAnyHeader()
            .Build()
        );
    }

    public static void UseDefaultEndpoints(this WebApplication app)
    {
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
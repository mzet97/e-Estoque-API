using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace e_Estoque_API.API.Configuration;

public static class ObservabilityConfig
{
    public static ResourceBuilder _resource;


    public static IServiceCollection AddObservability(
        this IServiceCollection services,
        string serviceName,
        string serviceVersion,
        IConfiguration configuration)
    {
        var resource = GetResource(serviceName, serviceVersion);

        var otelBuilder = services.AddOpenTelemetry();
        var uri = GetOtlpEndpoint(configuration);

        if (string.IsNullOrEmpty(uri))
            throw new ArgumentException("Otlp-Endpoint is required");

        otelBuilder
            .WithMetrics(metrics =>
            {
                metrics
                    .SetResourceBuilder(resource)
                    .AddRuntimeInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddEventCountersInstrumentation(c =>
                    {
                        c.AddEventSources(
                        "Microsoft.AspNetCore.Hosting",
                        "Microsoft-AspNetCore-Server-Kestrel",
                        "System.Net.Http",
                        "System.Net.Sockets");
                    })
                    .AddMeter("Microsoft.AspNetCore.Hosting")
                    .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                    .AddMeter("Microsoft.AspNetCore.Http.Connections")
                    .AddMeter("Microsoft.AspNetCore.Routing")
                    .AddMeter("Microsoft.AspNetCore.Diagnostics")
                    .AddMeter("Microsoft.AspNetCore.RateLimiting")
                    .AddPrometheusExporter()
                    .AddOtlpExporter(exporterOptions =>
                    {
                    exporterOptions.Endpoint = new Uri(uri);
                    })
                    .AddConsoleExporter();
            })
            .WithTracing(tracing =>
            {
                tracing.SetResourceBuilder(resource)
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddSqlClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation();

                tracing.AddOtlpExporter(exporterOptions => exporterOptions.Endpoint = new Uri(uri));
                tracing.AddConsoleExporter();
            });

        return services;
    }

    public static void MapObservability(this IEndpointRouteBuilder routes)
    {
        routes.MapPrometheusScrapingEndpoint();
    }

    public static void AddLogginOpenTelemetry(
        this ILoggingBuilder logging,
        string serviceName,
        string serviceVersion,
        IConfiguration configuration)
    {
        var uri = GetOtlpEndpoint(configuration);
        var resource = GetResource(serviceName, serviceVersion);
        logging.AddOpenTelemetry(options =>
        {
            options.IncludeScopes = true;
            options.ParseStateValues = true;

            options.SetResourceBuilder(resource)
                .AddOtlpExporter(otlpOptions =>
                {
                    otlpOptions.Endpoint = new Uri(uri);
                });
            options.AddConsoleExporter();
        });
    }


    private static string GetOtlpEndpoint(IConfiguration configuration)
    {
        var otlpEndpoint = configuration.GetSection("Otlp-Endpoint");
        return otlpEndpoint.Value ?? "";
    }

    private static ResourceBuilder GetResource(string serviceName, string serviceVersion)
    {
        if(_resource == null)
        {
            _resource = ResourceBuilder.CreateDefault().AddService(serviceName: serviceName, serviceVersion: serviceVersion);
        }

        return _resource;
    }
}
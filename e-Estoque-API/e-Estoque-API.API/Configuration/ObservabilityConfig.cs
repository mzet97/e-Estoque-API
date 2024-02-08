using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace e_Estoque_API.API.Configuration;

public static class ObservabilityConfig
{
    public static IServiceCollection AddObservability(this IServiceCollection services, string serviceName, IConfiguration configuration)
    {
        var resource = ResourceBuilder.CreateDefault().AddService(serviceName: serviceName, serviceVersion: "1.0");

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
                  .AddMeter("Microsoft.AspNetCore.Hosting", "Microsoft.AspNetCore.Server.Kestrel")
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
            });

        return services;
    }

    public static void MapObservability(this IEndpointRouteBuilder routes)
    {
        routes.MapPrometheusScrapingEndpoint();
    }

    private static string GetOtlpEndpoint(IConfiguration configuration)
    {
        var otlpEndpoint = configuration.GetSection("Otlp-Endpoint");
        return otlpEndpoint.Value ?? "";
    }
}
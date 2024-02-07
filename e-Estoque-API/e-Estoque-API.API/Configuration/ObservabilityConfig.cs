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
                  .AddPrometheusExporter();
            })
            .WithTracing(tracing =>
            {
                tracing.SetResourceBuilder(resource)
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddSqlClientInstrumentation();
            });

        return services;
    }

    public static void MapObservability(this IEndpointRouteBuilder routes)
    {
        routes.MapPrometheusScrapingEndpoint();
    }
}
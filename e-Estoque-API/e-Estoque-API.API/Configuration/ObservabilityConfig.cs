﻿using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace e_Estoque_API.API.Configuration
{
    public static class ObservabilityConfig
    {
        public static IServiceCollection AddObservability(this IServiceCollection services, string serviceName, IConfiguration configuration)
        {
            // create the resource that references the service name passed in
            var resource = ResourceBuilder.CreateDefault().AddService(serviceName: serviceName, serviceVersion: "1.0");

            // add the OpenTelemetry services
            var otelBuilder = services.AddOpenTelemetry();

            otelBuilder
                // add the metrics providers
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
                // add the tracing providers
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
}

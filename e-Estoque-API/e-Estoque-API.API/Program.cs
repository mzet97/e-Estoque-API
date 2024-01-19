using e_Estoque_API.API.Extensions;
using e_Estoque_API.Application.Configuration;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Sdk.Admin;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using e_Estoque_API.Infrastructure.Configuration;
using e_Estoque_API.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddMemoryCache();

builder.Services.AddMiniProfiler(options =>
{
    options.RouteBasePath = "/profiler";
    options.PopupRenderPosition = StackExchange.Profiling.RenderPosition.BottomLeft;
    options.PopupShowTimeWithChildren = true;
}).AddEntityFramework();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Development",
        builder =>
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());


    options.AddPolicy("Production",
        builder =>
            builder
                .WithMethods("GET")
                .WithOrigins("http://mzet97.dev")
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                //.WithHeaders(HeaderNames.ContentType, "x-custom-header")
                .AllowAnyHeader());
});


builder.Services.AddDbContextConfig(builder.Configuration);
builder.Services.ResolveDependencies();
builder.Services.AddMessageBus(builder.Configuration);
builder.Services.AddHandlers();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();
builder.Services.AddSwaggerGen();


var authenticationOptions = builder
                            .Configuration
                            .GetSection(KeycloakAuthenticationOptions.Section)
                            .Get<KeycloakAuthenticationOptions>();

builder.Services.AddKeycloakAuthentication(authenticationOptions);

var authorizationOptions = builder
                            .Configuration
                            .GetSection(KeycloakProtectionClientOptions.Section)
                            .Get<KeycloakProtectionClientOptions>();

builder.Services.AddKeycloakAuthorization(authorizationOptions);

var adminClientOptions = builder
                            .Configuration
                            .GetSection(KeycloakAdminClientOptions.Section)
                            .Get<KeycloakAdminClientOptions>();

builder.Services.AddKeycloakAdminHttpClient(adminClientOptions);


const string serviceName = "WeatherForecast";

builder.Logging.AddOpenTelemetry(options =>
{
    options
        .SetResourceBuilder(
            ResourceBuilder.CreateDefault()
                .AddService(serviceName))
        .AddConsoleExporter();
});

builder.Services.AddOpenTelemetry()
      .ConfigureResource(resource => resource.AddService(serviceName))
      .WithTracing(tracing => tracing
          .AddAspNetCoreInstrumentation()
          .AddConsoleExporter())
      .WithMetrics(metrics => metrics
          .AddAspNetCoreInstrumentation()
          .AddConsoleExporter());


var app = builder.Build();

//app.UseHttpLogging();
app.UseMiniProfiler();

if (app.Environment.IsDevelopment())
{
    //app.UseCors("Development");
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwaggerUI();
}
else
{
    //app.UseCors("Production");
    app.UseHsts();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseRouting();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();
app.UseSwaggerConfig();

app.Run();


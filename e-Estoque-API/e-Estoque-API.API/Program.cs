using e_Estoque_API.API.Configuration;
using e_Estoque_API.API.Extensions;
using e_Estoque_API.Application.Configuration;
using e_Estoque_API.Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc;

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

builder.Services.ResolveDependencies();
builder.Services.AddIdentityConfig(builder.Configuration);
builder.Services.AddMessageBus(builder.Configuration);
builder.Services.AddHandlers();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();

var app = builder.Build();

//app.UseHttpLogging();
app.UseMiniProfiler();

if (app.Environment.IsDevelopment())
{
    app.UseCors("Development");
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseCors("Production");
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


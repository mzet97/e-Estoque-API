using e_Estoque_API.API.Configuration;
using e_Estoque_API.API.Extensions;
using e_Estoque_API.Application;
using e_Estoque_API.Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddMemoryCache();


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

//builder.Services.AddMiniProfilerConfig();
builder.Services.AddCorsConfig();
builder.Services.AddDbContextConfig(builder.Configuration);
builder.Services.ResolveDependencies();
builder.Services.AddMessageBus(builder.Configuration);
builder.Services.AddApplicationServices();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();
builder.Services.AddSwaggerGen();
builder.Services.AddKeycloakConfig(builder.Configuration);

builder.Services.AddObservability("E-estoque", builder.Configuration);

var app = builder.Build();

app.MapObservability();
//app.UseHttpLogging();
//app.UseMiniProfiler();

if (app.Environment.IsDevelopment())
{
    //app.UseCors("Development");
    app.UseDeveloperExceptionPage();
}
else
{
    //app.UseCors("Production");
    app.UseHsts();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseExceptionHandler(options => { });
app.UseRouting();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseHealthChecks("/health");
app.UseStaticFiles();

app.MapControllers();
app.UseSwaggerConfig();

app.Run();


using e_Estoque_API.API.Configuration;
using e_Estoque_API.API.Extensions;
using e_Estoque_API.Application;
using e_Estoque_API.Infrastructure.Configuration;
using e_Estoque_API.Infrastructure.Persistence;
using e_Estoque_API.Infrastructure.Persistence.OData;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;

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

builder.Services.AddCorsConfig();
builder.Services.AddDbContextConfig(builder.Configuration);
builder.Services.ResolveDependenciesInfrastructure();
builder.Services.AddMessageBus(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services
    .AddControllers()
    .AddOData(opt => opt
        .AddRouteComponents("odata", ODataModel.Model) 
        .Select()                                      
        .Filter()                                      
        .OrderBy()                                      
        .Expand()                                       
        .Count()                                        
        .SetMaxTop(null)                                
    );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});
builder.Services.AddKeycloakConfig(builder.Configuration);
builder.Services.AddTransient<IClaimsTransformation, KeycloakClaimsTransformation>();
builder.Services.PostConfigure<JwtBearerOptions>(
    JwtBearerDefaults.AuthenticationScheme,
    options =>
    {
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters.AudienceValidator =
            (IEnumerable<string> tokenAudiences, SecurityToken securityToken, TokenValidationParameters validationParameters) =>
            {
                if (tokenAudiences == null)
                    return false;
                return tokenAudiences.Contains("e-estoque-client");
            };

    });
//builder.Services.AddWso2Config(builder.Configuration);

builder.Services.AddHealthChecks();

builder.Services.AddObservability("E-estoque", "1", builder.Configuration);
builder.Logging.AddLogginOpenTelemetry("E-estoque", "1", builder.Configuration);

var app = builder.Build();

app.MapObservability();

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
app.UseHealthChecks("/health");
app.UseStaticFiles();

app.MapControllers();
app.UseSwaggerConfig();

app.Run();
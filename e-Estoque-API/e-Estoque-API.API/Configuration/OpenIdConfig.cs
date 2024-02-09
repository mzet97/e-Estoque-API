using e_Estoque_API.API.Extensions.Auth;
using e_Estoque_API.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace e_Estoque_API.API.Configuration
{
    public static class OpenIdConfig
    {
        public static void AddWso2Config(this IServiceCollection services, IConfiguration configuration)
        {
            var wso2Config = new Wso2();
            configuration.GetSection("WSO2").Bind(wso2Config);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = wso2Config.Domain;
                    options.Audience = wso2Config.Audience;
                    options.MetadataAddress = wso2Config.Domain + wso2Config.MetadataAddress;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Create", policy => policy.Requirements.Add(new
                HasScopeRequirement("Create", wso2Config.Domain)));
            });

            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
        }
    }
}

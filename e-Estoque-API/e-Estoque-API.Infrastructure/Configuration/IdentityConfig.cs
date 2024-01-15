using e_Estoque_API.Core.Entities;
using e_Estoque_API.Infrastructure.Context;
using e_Estoque_API.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace e_Estoque_API.Infrastructure.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfig(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole()
                    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
                loggingBuilder.AddDebug();
            });

            services.AddDbContext<DataIdentityDbContext>(options =>
            {
                //options.UseLazyLoadingProxies();
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging(true);
            });

            services.AddDefaultIdentity<User>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                options.SignIn.RequireConfirmedAccount = true;

                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%&*()_=?. ";
                options.User.RequireUniqueEmail = true;
            })
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<DataIdentityDbContext>()
                .AddDefaultTokenProviders();

            // JWT
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();

            if(appSettings == null)
            {
                throw new ArgumentNullException("AppSettings configuration is missing");
            }

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSettings.ValidOn,
                    ValidIssuer = appSettings.Issuer
                };
            });

            return services;
        }
    }
}

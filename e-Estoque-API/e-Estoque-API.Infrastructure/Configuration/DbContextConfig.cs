using e_Estoque_API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace e_Estoque_API.Infrastructure.Configuration;

public static class DbContextConfig
{
    public static IServiceCollection AddDbContextConfig(this IServiceCollection services,
       IConfiguration configuration)
    {
        services.AddDbContext<EstoqueDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            //options.EnableSensitiveDataLogging(true);
        });

        services.AddHealthChecks()
            .AddDbContextCheck<EstoqueDbContext>();

        return services;
    }
}
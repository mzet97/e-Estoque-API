namespace e_Estoque_API.API.Configuration;

public static class CorsConfig
{
    public static IServiceCollection AddCorsConfig(this IServiceCollection services)
    {
        services.AddCors(options =>
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
                        .WithOrigins("https://mzet97.dev")
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        //.WithHeaders(HeaderNames.ContentType, "x-custom-header")
                        .AllowAnyHeader());
        });

        return services;
    }
}
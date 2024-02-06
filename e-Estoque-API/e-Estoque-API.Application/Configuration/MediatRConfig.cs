using e_Estoque_API.Application.Categories.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace e_Estoque_API.Application.Configuration
{
    public static class MediatRConfig
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCategoryCommand).Assembly));

            return services;
        }
    }
}

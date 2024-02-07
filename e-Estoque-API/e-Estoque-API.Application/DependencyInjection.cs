using e_Estoque_API.Application.Common.Behaviours;
using e_Estoque_API.Application.Dtos.ViewModels;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace e_Estoque_API.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(BaseViewModel).Assembly);

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(BaseViewModel).Assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });

        return services;
    }
}
namespace e_Estoque_API.API.Configuration;

public static class MiniProfilerConfig
{
    public static IServiceCollection AddMiniProfilerConfig(this IServiceCollection services)
    {
        services.AddMiniProfiler(options =>
        {
            options.RouteBasePath = "/profiler";
            options.PopupRenderPosition = StackExchange.Profiling.RenderPosition.BottomLeft;
            options.PopupShowTimeWithChildren = true;
        }).AddEntityFramework();

        return services;
    }
}
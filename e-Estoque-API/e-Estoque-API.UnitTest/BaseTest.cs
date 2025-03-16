using e_Estoque_API.Infrastructure.Configuration;
using e_Estoque_API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace e_Estoque_API.UnitTest;

public class BaseTest : IDisposable
{
    public ServiceProvider ServiceProvider { get; private set; }

    public BaseTest()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddDbContext<EstoqueDbContext>(options =>
        {
            options.UseNpgsql($"Host=ZETDEVSERVER;Port=5432;Pooling=true;Database=e-estoque;User Id=postgres;Password=dsv@123;");
        },ServiceLifetime.Scoped);

        serviceCollection.ResolveDependenciesInfrastructure();

        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    public void Dispose(bool disposing)
    {
        if (disposing)
        {
            ServiceProvider?.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

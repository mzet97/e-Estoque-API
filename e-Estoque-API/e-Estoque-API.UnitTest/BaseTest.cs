using e_Estoque_API.Infrastructure.Configuration;
using e_Estoque_API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace e_Estoque_API.UnitTest
{
    public class BaseTest : IDisposable
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public BaseTest()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<EstoqueDbContext>(o =>
                o.UseNpgsql($"Host=localhost;Port=5432;Pooling=true;Database=e-estoque-test;User Id=postgres;Password=dsv@123;"),
                  ServiceLifetime.Transient
            );

            serviceCollection.ResolveDependencies();

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
}

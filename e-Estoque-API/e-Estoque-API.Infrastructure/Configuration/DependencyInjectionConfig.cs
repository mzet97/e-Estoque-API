using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.Persistence;
using e_Estoque_API.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace e_Estoque_API.Infrastructure.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        services.AddScoped<EstoqueDbContext>();

        #region Repository

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISaleProductRepository, SaleProductRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
        services.AddScoped<ITaxRepository, TaxRepository>();

        #endregion Repository

        return services;
    }
}
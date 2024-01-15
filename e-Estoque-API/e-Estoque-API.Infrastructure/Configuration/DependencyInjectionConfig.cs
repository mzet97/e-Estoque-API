using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.Context;
using e_Estoque_API.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace e_Estoque_API.Infrastructure.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<DataIdentityDbContext>();

            #region Repository
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISaleProductRepository, SaleProductRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<ITaxRepository, TaxRepository>();

            #endregion

            return services;
        }
    }
}

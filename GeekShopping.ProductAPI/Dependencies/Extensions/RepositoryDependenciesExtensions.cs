using GeekShopping.ProductAPI.Repositories;

namespace GeekShopping.ProductAPI.Dependencies.Extensions
{
    public static class RepositoryDependenciesExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}

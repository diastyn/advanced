using Advanced.Domain.Repositories.Common;
using Advanced.Domain.Repositories.Products;
using Advanced.Domain.Repositories.Users;
using Advanced.Persistence.Repositories.Common;
using Advanced.Persistence.Repositories.Products;
using Advanced.Persistence.Repositories.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Advanced.Persistence.ServiceRegisters;

public static class RepositoryRegister
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(ICommonRepository<>), typeof(CommonRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserHistoryRepository, UserHistoryRepository>();
    }
}
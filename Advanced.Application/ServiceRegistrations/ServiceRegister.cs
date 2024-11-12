using Advanced.Application.Services.Implementations;
using Advanced.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Advanced.Application.ServiceRegistrations;

public static class ServiceRegister
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUserService, UserService>();
    }
}
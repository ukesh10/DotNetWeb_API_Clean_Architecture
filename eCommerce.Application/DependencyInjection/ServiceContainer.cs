using eCommerce.Application.Mapping;
using eCommerce.Application.Services.Implementation;
using eCommerce.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Application.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingConfig));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            //services.AddScoped<IOrderService, OrderService>();
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IRoleService, RoleService>();
            //services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}

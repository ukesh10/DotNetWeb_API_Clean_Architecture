using eCommerce.Application.Mapping;
using eCommerce.Application.Services.Implementation;
using eCommerce.Application.Services.Implementation.Authentication;
using eCommerce.Application.Services.Interfaces;
using eCommerce.Application.Services.Interfaces.Authentication;
using eCommerce.Application.Validations;
using eCommerce.Application.Validations.Authentication;
using FluentValidation;
using FluentValidation.AspNetCore;
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

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;
        }
    }
}

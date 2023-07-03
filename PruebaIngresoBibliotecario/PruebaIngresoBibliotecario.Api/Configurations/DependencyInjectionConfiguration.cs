using Microsoft.Extensions.DependencyInjection;
using PruebaIngresoBibliotecario.Application.IoC;
using PruebaIngresoBibliotecario.Infrastructure.IoC;


namespace PruebaIngresoBibliotecario.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddInfrastructure();
            services.AddApplication();

            return services;
        }
    }

}


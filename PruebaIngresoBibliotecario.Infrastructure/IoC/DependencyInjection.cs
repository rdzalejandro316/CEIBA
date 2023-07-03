using Microsoft.Extensions.DependencyInjection;
using PruebaIngresoBibliotecario.Infrastructure.DataAccess;
using PruebaIngresoBibliotecario.Infrastructure.Interfaces;

namespace PruebaIngresoBibliotecario.Infrastructure.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}

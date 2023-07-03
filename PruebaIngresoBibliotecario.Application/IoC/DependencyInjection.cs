using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PruebaIngresoBibliotecario.Application.Common.Mapper;
using System.Reflection;

namespace PruebaIngresoBibliotecario.Application.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddMediatR(Assembly.GetExecutingAssembly());           
            return services;
        }
    }
}

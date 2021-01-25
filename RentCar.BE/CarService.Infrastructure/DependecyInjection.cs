using CarService.Core.Interfaces;
using CarService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CarService.Infrastructure
{
    public static class DependecyInjection
    {
       
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ICarRepository, CarRepository>();
            services.AddTransient<ITypeOfFuelRepository, TypeOfFuelRepository>();
            services.AddTransient<ITypeOfCarRepository, TypeOfCarRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<IModelRepository, ModelRepository>();
            return services;
        }
    }
}

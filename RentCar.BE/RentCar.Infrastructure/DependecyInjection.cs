using RentCar.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using RentCar.Infrastructure.Repositories;

namespace RentCar.Infrastructure
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

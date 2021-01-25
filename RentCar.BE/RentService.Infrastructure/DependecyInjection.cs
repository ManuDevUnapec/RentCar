using System;
using Microsoft.Extensions.DependencyInjection;
using RentService.Core.Interfaces;
using RentService.Infrastructure.Repositories;

namespace RentService.Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IRentRepository, RentRepository>();
            return services;
        }
    }
}

using System;
using InspectionService.Core.Interfaces;
using InspectionService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace InspectionService.Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IInspectionRepository, InspectionRepository>();
            return services;
        }
    }
}

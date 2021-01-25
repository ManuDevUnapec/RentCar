using System;
using EmployeeService.Core.Interfaces;
using EmployeeService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeService.Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            return services;
        }
    }
}

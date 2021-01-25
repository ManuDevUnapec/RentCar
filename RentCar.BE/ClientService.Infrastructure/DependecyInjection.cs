using System;
using ClientService.Core.Interfaces;
using ClientService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ClientService.Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IClientRepository, ClientRepository>();
            return services;
        }
    }
}

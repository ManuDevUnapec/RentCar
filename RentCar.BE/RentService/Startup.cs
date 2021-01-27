using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RentService.Core.Interfaces;
using RentService.Infrastructure;
using RentService.Infrastructure.Repositories;
using RentService.Queues;

namespace RentService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RentService", Version = "v1" });
            });

            //MassTransit
            services.AddMassTransit(config => {
                config.AddConsumer<ClientConsumer>();
                config.AddConsumer<EmployeeConsumer>();

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(Configuration["Queues:RabbitMQ:DefaultHost:Host"]);

                    cfg.ReceiveEndpoint(Configuration["Queues:RabbitMQ:DefaultHost:ClientQueue"], c =>
                    {
                        c.ConfigureConsumer<ClientConsumer>(ctx);
                    });

                    cfg.ReceiveEndpoint(Configuration["Queues:RabbitMQ:DefaultHost:EmployeeQueue"], c => {
                        c.ConfigureConsumer<EmployeeConsumer>(ctx);
                    });
                });
            });

            services.AddMassTransitHostedService();

            //Dependecy Injections
            services.AddTransient<IRentRepository, RentRepository>();

            //Adding the Dependecy Injections for the Rent Infrastructue
            services.AddInfrastructure();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RentService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

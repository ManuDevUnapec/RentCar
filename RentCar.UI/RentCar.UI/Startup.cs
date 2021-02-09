using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RentCar.UI.Data;
using RentCar.UI.Data.Cars.Brands.Services;
using RentCar.UI.Data.Cars.Cars.Services;
using RentCar.UI.Data.Cars.Models.Services;
using RentCar.UI.Data.Cars.TypeOfCars.Services;
using RentCar.UI.Data.Cars.TypeOfFuels.Services;
using RentCar.UI.Data.Clients.Clients.Services;
using RentCar.UI.Data.Employees.Employees.Services;
using RentCar.UI.Data.Rents.Services;

namespace RentCar.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            //Http client Factory
            services.AddHttpClient();
            services.AddHttpClient("Cars", config => {
                config.BaseAddress = new Uri(Configuration["ApiGateway:RentCar:Cars"]);
            });
            services.AddHttpClient("Clients", config => {
                config.BaseAddress = new Uri(Configuration["ApiGateway:RentCar:Clients"]);
            });

            services.AddHttpClient("Employees", config => {
                config.BaseAddress = new Uri(Configuration["ApiGateway:RentCar:Employees"]);
            });

            services.AddHttpClient("Rents", config => {
                config.BaseAddress = new Uri(Configuration["ApiGateway:RentCar:Rents"]);
            });

            //Cars
            services.AddSingleton<TypeOfCarService>();
            services.AddSingleton<TypeOfFuelService>();
            services.AddSingleton<BrandService>();
            services.AddSingleton<ModelService>();
            services.AddSingleton<CarService>();
            services.AddSingleton<EmployeeService>();
            services.AddSingleton<RentService>();

            //Clients
            services.AddSingleton<ClientService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}

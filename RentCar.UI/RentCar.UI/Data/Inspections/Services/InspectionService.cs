using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using RentCar.UI.Data.Cars.Cars.Models;
using RentCar.UI.Data.Cars.Cars.Services;
using RentCar.UI.Data.Clients.Clients.Models;
using RentCar.UI.Data.Clients.Clients.Services;
using RentCar.UI.Data.Employees.Employees.Models;
using RentCar.UI.Data.Employees.Employees.Services;
using RentCar.UI.Data.Inspections.Models;

namespace RentCar.UI.Data.Inspections.Services
{
    public class InspectionService
    {
        IHttpClientFactory _carFactory { get; set; }
        ClientService _clientService { get; set; }
        EmployeeService _employeeService { get; set; }
        CarService _carService { get; set; }

        public InspectionService(IHttpClientFactory carFactory, ClientService clientService,
            EmployeeService employeeService, CarService carService)
        {
            _carFactory = carFactory;
            _clientService = clientService;
            _employeeService = employeeService;
            _carService = carService;
        }

        public async Task<List<Inspection>> GetAll()
        {
            try
            {
                var client = _carFactory.CreateClient("Inspections");
                var cars = await client.GetFromJsonAsync<List<Inspection>>("Inspection");
                return cars;
            }
            catch (Exception e)
            {
                //Log error
                return new List<Inspection>();
            }
        }

        public async Task<Inspection> GetByID(int id)
        {
            try
            {
                var client = _carFactory.CreateClient("Inspections");
                var inspection = await client.GetFromJsonAsync<Inspection>($"Inspection/{id}");
                return inspection;
            }
            catch (Exception e)
            {
                //Log error
                return new Inspection();
            }
        }

        public async Task<HttpResponseMessage> Create(Inspection inspection)
        {
            try
            {
                Client client = new Client();
                Employee employee = new Employee();
                Car car = car = new Car();

                client = await _clientService.GetByID((int)inspection.ClientID);
                employee = await _employeeService.GetByID((int)inspection.EmployeeID);
                car = await _carService.GetByID((int)inspection.CarID);
                inspection.Client = client.Name;
                inspection.Employee = employee.Name;
                inspection.Car = car.Description;
                inspection.InspectionDate = DateTime.Now;

                var clientFactory = _carFactory.CreateClient("Inspections");
                var response = await clientFactory.PostAsJsonAsync<Inspection>("Inspection", inspection);
                return response;
            }
            catch (Exception e)
            {
                //Log error
                throw e;
            }
        }

        public async Task<HttpResponseMessage> Put(Inspection inspection)
        {
            try
            {
                Client client = new Client();
                Employee employee = new Employee();
                Car car = car = new Car();

                client = await _clientService.GetByID((int)inspection.ClientID);
                employee = await _employeeService.GetByID((int)inspection.EmployeeID);
                car = await _carService.GetByID((int)inspection.CarID);
                inspection.Client = client.Name;
                inspection.Employee = employee.Name;
                inspection.Car = car.Description;

                var clientFactory = _carFactory.CreateClient("Inspections");
                var response = await clientFactory.PutAsJsonAsync<Inspection>("Inspection", inspection);
                return response;
            }
            catch (Exception e)
            {
                //Log error
                throw e;
            }
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                var client = _carFactory.CreateClient("Inspections");
                var response = await client.DeleteAsync($"Inspection/{id}");
                return response;
            }
            catch (Exception e)
            {
                //Log error
                throw e;
            }
        }
    }
}

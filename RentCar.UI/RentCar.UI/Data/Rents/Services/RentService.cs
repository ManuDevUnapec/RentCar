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
using RentCar.UI.Data.Rents.Models;

namespace RentCar.UI.Data.Rents.Services
{
    public class RentService
    {
        IHttpClientFactory _clientFactory { get; set; }
        ClientService _clientService { get; set; }
        EmployeeService _employeeService { get; set; }
        CarService _carService { get; set; }

        public RentService(IHttpClientFactory clientFactory, ClientService clientService,
            EmployeeService employeeService, CarService carService)
        {
            _clientFactory = clientFactory;
            _clientService = clientService;
            _employeeService = employeeService;
            _carService = carService;
        }

        public async Task<List<Rent>> GetAll()
        {
            try
            {
                var client = _clientFactory.CreateClient("Rents");
                var rents = await client.GetFromJsonAsync<List<Rent>>("Rent");
                return rents;
            }
            catch (Exception e)
            {
                //Log error
                return new List<Rent>();
            }
        }

        public async Task<Rent> GetByID(int id)
        {
            try
            {
                var client = _clientFactory.CreateClient("Rents");
                var model = await client.GetFromJsonAsync<Rent>($"Rent/{id}");
                return model;
            }
            catch (Exception e)
            {
                //Log error
                return new Rent();
            }
        }

        public async Task<HttpResponseMessage> Create(Rent rent)
        {
            try
            {
                Client client = new Client();
                Employee employee = new Employee();
                Car car = car = new Car();

                client = await _clientService.GetByID((int)rent.ClientID);
                employee = await _employeeService.GetByID((int)rent.EmployeeID);
                car = await _carService.GetByID((int)rent.CarID);
                rent.Client = client.Name;
                rent.Employee = employee.Name;
                rent.Car = car.Description;
                rent.Status = RentStatus.Rented.ToString();

                var clientFactory = _clientFactory.CreateClient("Rents");
                var response = await clientFactory.PostAsJsonAsync<Rent>("Rent", rent);
                return response;
            }
            catch (Exception e)
            {
                //Log error
                throw e;
            }
        }

        public async Task<HttpResponseMessage> Put(Rent rent)
        {
            try
            {
                Client client = new Client();
                Employee employee = new Employee();
                Car car = car = new Car();

                client = await _clientService.GetByID((int)rent.ClientID);
                employee = await _employeeService.GetByID((int)rent.EmployeeID);
                car = await _carService.GetByID((int)rent.CarID);
                rent.Client = client.Name;
                rent.Employee = employee.Name;
                rent.Car = car.Description;

                var clientFactory = _clientFactory.CreateClient("Rents");
                var response = await clientFactory.PutAsJsonAsync<Rent>("Rent", rent);
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
                var client = _clientFactory.CreateClient("Rents");
                var response = await client.DeleteAsync($"Rent/{id}");
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

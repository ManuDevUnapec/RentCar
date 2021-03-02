using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
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
        IHttpClientFactory _clientFactory { get; set; }
        ClientService _clientService { get; set; }
        EmployeeService _employeeService { get; set; }
        CarService _carService { get; set; }

        public InspectionService(IHttpClientFactory clientFactory, ClientService clientService,
            EmployeeService employeeService, CarService carService)
        {
            _clientFactory = clientFactory;
            _clientService = clientService;
            _employeeService = employeeService;
            _carService = carService;
        }

        public async Task<List<Inspection>> GetAll()
        {
            try
            {
                var client = _clientFactory.CreateClient("Inspections");
                var inspections = await client.GetFromJsonAsync<List<Inspection>>("Inspection");
                inspections.Reverse();
                return inspections;
            }
            catch (Exception)
            {
                return new List<Inspection>();
            }
        }

        public async Task<Inspection> GetByID(int id)
        {
            try
            {
                var client = _clientFactory.CreateClient("Inspections");
                var inspection = await client.GetFromJsonAsync<Inspection>($"Inspection/{id}");
                return inspection;
            }
            catch (Exception)
            {
                return new Inspection();
            }
        }

        public async Task<List<Inspection>> GetReport(int? id, bool? grated, bool? cat, bool? rubberBack,
            bool? glassBreak, bool? rubberStateOne, bool? rubberStateTwo, bool? rubberStateThree,
            bool? rubberStateFourth, string amountOfFuel, string status, int? employeeID,
            int? clientID, int? carID)
        {
            try
            {
                var client = _clientFactory.CreateClient("Inspections");
                var parameters = new Dictionary<string, string>();
                parameters.Add("id", id.ToString());
                parameters.Add("grated", grated.ToString()); ;
                parameters.Add("cat", cat.ToString());
                parameters.Add("rubberBack", rubberBack.ToString());
                parameters.Add("glassBreak", glassBreak.ToString());
                parameters.Add("rubberStateOne", rubberStateOne.ToString());
                parameters.Add("rubberStateTwo", rubberStateTwo.ToString());
                parameters.Add("rubberStateThree", rubberStateThree.ToString());
                parameters.Add("rubberStateFourth", rubberStateFourth.ToString());
                parameters.Add("amountOfFuel", amountOfFuel);
                parameters.Add("status", status);
                parameters.Add("employeeId", employeeID.ToString());
                parameters.Add("clientId", clientID.ToString());
                parameters.Add("carId", carID.ToString());
                var url = new Uri(QueryHelpers.AddQueryString($"{client.BaseAddress}Inspection/GetReport", parameters));
                var inspections = await client.GetFromJsonAsync<List<Inspection>>(url);
                inspections.Reverse();
                return inspections;
            }
            catch (Exception)
            {
                return new List<Inspection>();
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

                var clientFactory = _clientFactory.CreateClient("Inspections");
                var response = await clientFactory.PostAsJsonAsync<Inspection>("Inspection", inspection);
                return response;
            }
            catch (Exception)
            {
                throw;
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

                var clientFactory = _clientFactory.CreateClient("Inspections");
                var response = await clientFactory.PutAsJsonAsync<Inspection>("Inspection", inspection);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                var client = _clientFactory.CreateClient("Inspections");
                var response = await client.DeleteAsync($"Inspection/{id}");
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

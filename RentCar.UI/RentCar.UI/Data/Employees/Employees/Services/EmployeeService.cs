using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using RentCar.UI.Data.Employees.Employees.Models;

namespace RentCar.UI.Data.Employees.Employees.Services
{
    public class EmployeeService
    {
        IHttpClientFactory _clientFactory { get; set; }

        public EmployeeService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<Employee>> GetAll()
        {
            try
            {
                var client = _clientFactory.CreateClient("Employees");
                var employees = await client.GetFromJsonAsync<List<Employee>>("Employee");
                return employees;
            }
            catch (Exception e)
            {
                //Log error
                return new List<Employee>();
            }
        }

        public async Task<Employee> GetByID(int id)
        {
            try
            {
                var client = _clientFactory.CreateClient("Employees");
                var model = await client.GetFromJsonAsync<Employee>($"Employee/{id}");
                return model;
            }
            catch (Exception e)
            {
                //Log error
                return new Employee();
            }
        }

        public async Task<HttpResponseMessage> Create(Employee employee)
        {
            try
            {
                var client = _clientFactory.CreateClient("Employees");
                var response = await client.PostAsJsonAsync<Employee>("Employee", employee);
                return response;
            }
            catch (Exception e)
            {
                //Log error
                throw e;
            }
        }

        public async Task<HttpResponseMessage> Put(Employee employee)
        {
            try
            {
                var client = _clientFactory.CreateClient("Employees");
                var response = await client.PutAsJsonAsync<Employee>("Employee", employee);
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
                var client = _clientFactory.CreateClient("Employees");
                var response = await client.DeleteAsync($"Employee/{id}");
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

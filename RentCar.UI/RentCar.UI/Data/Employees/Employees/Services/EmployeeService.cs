using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
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
                employees.Reverse();
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

        public async Task<List<Employee>> GetReport(int? id, string name, string identificationCard,
            string hourHand, string commisionPercent, string status)
        {
            try
            {
                var client = _clientFactory.CreateClient("Employees");
                var parameters = new Dictionary<string, string>();
                parameters.Add("id", id.ToString());
                parameters.Add("name", name);
                parameters.Add("identificationCard", identificationCard);
                parameters.Add("hourHand", hourHand);
                parameters.Add("commisionPercent", commisionPercent);
                parameters.Add("status", status);
                var url = new Uri(QueryHelpers.AddQueryString($"{client.BaseAddress}Employee/GetReport", parameters));
                var employees = await client.GetFromJsonAsync<List<Employee>>(url);
                employees.Reverse();
                return employees;
            }
            catch (Exception e)
            {
                //Log error
                return new List<Employee>();
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

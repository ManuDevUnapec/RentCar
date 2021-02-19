using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using RentCar.UI.Data.Cars.Cars.Models;

namespace RentCar.UI.Data.Cars.Cars.Services
{
    public class CarService
    {
        IHttpClientFactory _carFactory { get; set; }

        public CarService(IHttpClientFactory carFactory)
        {
            _carFactory = carFactory;
        }

        public async Task<List<Car>> GetAll()
        {
            try
            {
                var client = _carFactory.CreateClient("Cars");
                var cars = await client.GetFromJsonAsync<List<Car>>("Car");
                cars.Reverse();
                return cars;
            }
            catch (Exception e)
            {
                //Log error
                return new List<Car>();
            }
        }

        public async Task<Car> GetByID(int id)
        {
            try
            {
                var client = _carFactory.CreateClient("Cars");
                var car = await client.GetFromJsonAsync<Car>($"Car/{id}");
                return car;
            }
            catch (Exception e)
            {
                //Log error
                return new Car();
            }
        }

        public async Task<List<Car>> GetReport(int? id, string description, string status,
            int? brandID, int? modelID, int? typeOfCarID, int? typeOfFuelID,
            string plateNumber, string engineNumber, string chassisNumber)
        {
            try
            {
                var client = _carFactory.CreateClient("Cars");
                var parameters = new Dictionary<string, string>();
                parameters.Add("id", id.ToString());
                parameters.Add("description", description);
                parameters.Add("status", status);
                parameters.Add("brandId", brandID.ToString());
                parameters.Add("modelId", modelID.ToString());
                parameters.Add("typeOfCarID", typeOfCarID.ToString());
                parameters.Add("typeOfFuelID", typeOfFuelID.ToString());
                parameters.Add("plateNumber", plateNumber);
                parameters.Add("engineNumber", engineNumber);
                parameters.Add("chassisNumber", chassisNumber);
                var url = new Uri(QueryHelpers.AddQueryString($"{client.BaseAddress}Car/GetReport", parameters));
                var cars = await client.GetFromJsonAsync<List<Car>>(url);
                return cars;
            }
            catch (Exception e)
            {
                //Log error
                return new List<Car>();
            }
        }

        public async Task<HttpResponseMessage> Create(Car car)
        {
            try
            {
                var client = _carFactory.CreateClient("Cars");
                var response = await client.PostAsJsonAsync<Car>("Car", car);
                return response;
            }
            catch (Exception e)
            {
                //Log error
                throw e;
            }
        }

        public async Task<HttpResponseMessage> Put(Car car)
        {
            try
            {
                var client = _carFactory.CreateClient("Cars");
                var response = await client.PutAsJsonAsync<Car>("Car", car);
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
                var client = _carFactory.CreateClient("Cars");
                var response = await client.DeleteAsync($"Car/{id}");
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

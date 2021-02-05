using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using RentCar.UI.Data.Cars.Cars.Models;

namespace RentCar.UI.Data.Cars.Cars.Services
{
    public class CarService
    {
        IHttpClientFactory _clientFactory { get; set; }

        public CarService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<Car>> GetAll()
        {
            try
            {
                var client = _clientFactory.CreateClient("Cars");
                var cars = await client.GetFromJsonAsync<List<Car>>("Car");
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
                var client = _clientFactory.CreateClient("Cars");
                var car = await client.GetFromJsonAsync<Car>($"Car/{id}");
                return car;
            }
            catch (Exception e)
            {
                //Log error
                return new Car();
            }
        }

        public async Task<HttpResponseMessage> Create(Car car)
        {
            try
            {
                var client = _clientFactory.CreateClient("Cars");
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
                var client = _clientFactory.CreateClient("Cars");
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
                var client = _clientFactory.CreateClient("Cars");
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

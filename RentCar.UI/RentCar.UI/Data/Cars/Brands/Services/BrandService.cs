using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using RentCar.UI.Data.Cars.Brands.Models;

namespace RentCar.UI.Data.Cars.Brands.Services
{
    public class BrandService
    {
        IHttpClientFactory _carFactory { get; set; }

        public BrandService(IHttpClientFactory carFactory)
        {
            _carFactory = carFactory;
        }

        public async Task<List<Brand>> GetAll()
        {
            try
            {
                var client = _carFactory.CreateClient("Cars");
                var brands = await client.GetFromJsonAsync<List<Brand>>("Brand");
                return brands;
            }
            catch (Exception e)
            {
                //Log error
                return new List<Brand>();
            }
        }

        public async Task<Brand> GetByID(int id)
        {
            try
            {
                var client = _carFactory.CreateClient("Cars");
                var brand = await client.GetFromJsonAsync<Brand>($"Brand/{id}");
                return brand;
            }
            catch (Exception e)
            {
                //Log error
                return new Brand();
            }
        }

        public async Task<HttpResponseMessage> Create(Brand typeOfCar)
        {
            try
            {
                var client = _carFactory.CreateClient("Cars");
                var response = await client.PostAsJsonAsync<Brand>("Brand", typeOfCar);
                return response;
            }
            catch (Exception e)
            {
                //Log error
                throw e;
            }
        }

        public async Task<HttpResponseMessage> Put(Brand typeOfCar)
        {
            try
            {
                var client = _carFactory.CreateClient("Cars");
                var response = await client.PutAsJsonAsync<Brand>("Brand", typeOfCar);
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
                var response = await client.DeleteAsync($"Brand/{id}");
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

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using RentCar.UI.Data.Cars.TypeOfCars.Models;

namespace RentCar.UI.Data.Cars.TypeOfCars.Services
{
    public class TypeOfCarService
    {
        IHttpClientFactory _clientFactory { get; set; }

        public TypeOfCarService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<TypeOfCar>> GetAll()
        {
            try
            {
                var client = _clientFactory.CreateClient("Cars");
                var typeOfCars = await client.GetFromJsonAsync<List<TypeOfCar>>("TypeOfCar");
                return typeOfCars;
            }catch(Exception e)
            {
                //Log error
                return new List<TypeOfCar>();
            }
        }

        public async Task<TypeOfCar> GetByID(int id)
        {
            try
            {
                var client = _clientFactory.CreateClient("Cars");
                var typeOfCar = await client.GetFromJsonAsync<TypeOfCar>($"TypeOfCar/{id}");
                return typeOfCar;
            }
            catch (Exception e)
            {
                //Log error
                return new TypeOfCar();
            }
        }

        public async Task<HttpResponseMessage> Create(TypeOfCar typeOfCar)
        {
            try
            {
                var client = _clientFactory.CreateClient("Cars");
                var response = await client.PostAsJsonAsync<TypeOfCar>("TypeOfCar", typeOfCar);
                return response;
            }
            catch (Exception e)
            {
                //Log error
                throw e;
            }
        }

        public async Task<HttpResponseMessage> Put(TypeOfCar typeOfCar)
        {
            try
            {
                var client = _clientFactory.CreateClient("Cars");
                var response = await client.PutAsJsonAsync<TypeOfCar>("TypeOfCar", typeOfCar);
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
                var response = await client.DeleteAsync($"TypeOfCar/{id}");
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
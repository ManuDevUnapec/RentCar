using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using RentCar.UI.Data.Cars.Models.Models;

namespace RentCar.UI.Data.Cars.Models.Services
{
    public class ModelService
    {
        IHttpClientFactory _clientFactory { get; set; }

        public ModelService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<Model>> GetAll()
        {
            try
            {
                var client = _clientFactory.CreateClient("Cars");
                var models = await client.GetFromJsonAsync<List<Model>>("Model");
                return models;
            }
            catch (Exception e)
            {
                //Log error
                return new List<Model>();
            }
        }

        public async Task<Model> GetByID(int id)
        {
            try
            {
                var client = _clientFactory.CreateClient("Cars");
                var model = await client.GetFromJsonAsync<Model>($"Model/{id}");
                return model;
            }
            catch (Exception e)
            {
                //Log error
                return new Model();
            }
        }

        public async Task<HttpResponseMessage> Create(Model model)
        {
            try
            {
                var client = _clientFactory.CreateClient("Cars");
                var response = await client.PostAsJsonAsync<Model>("Model", model);
                return response;
            }
            catch (Exception e)
            {
                //Log error
                throw e;
            }
        }

        public async Task<HttpResponseMessage> Put(Model model)
        {
            try
            {
                var client = _clientFactory.CreateClient("Cars");
                var response = await client.PutAsJsonAsync<Model>("Model", model);
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
                var response = await client.DeleteAsync($"Model/{id}");
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

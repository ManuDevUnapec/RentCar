using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using RentCar.UI.Data.Clients.Clients.Models;

namespace RentCar.UI.Data.Clients.Clients.Services
{
    public class ClientService
    {
        IHttpClientFactory _clientFactory { get; set; }

        public ClientService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<Client>> GetAll()
        {
            try
            {
                var client = _clientFactory.CreateClient("Clients");
                var brands = await client.GetFromJsonAsync<List<Client>>("Client");
                return brands;
            }
            catch (Exception e)
            {
                //Log error
                return new List<Client>();
            }
        }

        public async Task<Client> GetByID(int id)
        {
            try
            {
                var client = _clientFactory.CreateClient("Clients");
                var model = await client.GetFromJsonAsync<Client>($"Client/{id}");
                return model;
            }
            catch (Exception e)
            {
                //Log error
                return new Client();
            }
        }

        public async Task<HttpResponseMessage> Create(Client typeOfCar)
        {
            try
            {
                var client = _clientFactory.CreateClient("Clients");
                var response = await client.PostAsJsonAsync<Client>("Client", typeOfCar);
                return response;
            }
            catch (Exception e)
            {
                //Log error
                throw e;
            }
        }

        public async Task<HttpResponseMessage> Put(Client typeOfCar)
        {
            try
            {
                var client = _clientFactory.CreateClient("Clients");
                var response = await client.PutAsJsonAsync<Client>("Client", typeOfCar);
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
                var client = _clientFactory.CreateClient("Clients");
                var response = await client.DeleteAsync($"Client/{id}");
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

﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using RentCar.UI.Data.Cars.TypeOfFuels.Models;

namespace RentCar.UI.Data.Cars.TypeOfFuels.Services
{
    public class TypeOfFuelService
    {
        IHttpClientFactory _carFactory { get; set; }

        public TypeOfFuelService(IHttpClientFactory carFactory)
        {
            _carFactory = carFactory;
        }

        public async Task<List<TypeOfFuel>> GetAll()
        {
            try
            {
                var client = _carFactory.CreateClient("Cars");
                var typeOfFuels = await client.GetFromJsonAsync<List<TypeOfFuel>>("TypeOfFuel");
                return typeOfFuels;
            }
            catch (Exception e)
            {
                //Log error
                return new List<TypeOfFuel>();
            }
        }

        public async Task<TypeOfFuel> GetByID(int id)
        {
            try
            {
                var client = _carFactory.CreateClient("Cars");
                var typeOfFuel = await client.GetFromJsonAsync<TypeOfFuel>($"TypeOfFuel/{id}");
                return typeOfFuel;
            }
            catch (Exception e)
            {
                //Log error
                return new TypeOfFuel();
            }
        }

        public async Task<List<TypeOfFuel>> GetReport(int? id, string description, string status)
        {
            try
            {
                var client = _carFactory.CreateClient("Cars");
                var parameters = new Dictionary<string, string>();
                parameters.Add("id", id.ToString());
                parameters.Add("description", description);
                parameters.Add("status", status);
                var url = new Uri(QueryHelpers.AddQueryString($"{client.BaseAddress}TypeOfFuel/GetReport", parameters));
                var typeOfCars = await client.GetFromJsonAsync<List<TypeOfFuel>>(url);
                return typeOfCars;
            }
            catch (Exception e)
            {
                //Log error
                return new List<TypeOfFuel>();
            }
        }

        public async Task<HttpResponseMessage> Create(TypeOfFuel typeOfFuel)
        {
            try
            {
                var client = _carFactory.CreateClient("Cars");
                var response = await client.PostAsJsonAsync<TypeOfFuel>("TypeOfFuel", typeOfFuel);
                return response;
            }
            catch (Exception e)
            {
                //Log error
                throw e;
            }
        }

        public async Task<HttpResponseMessage> Put(TypeOfFuel typeOfFuel)
        {
            try
            {
                var client = _carFactory.CreateClient("Cars");
                var response = await client.PutAsJsonAsync<TypeOfFuel>("TypeOfFuel", typeOfFuel);
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
                var response = await client.DeleteAsync($"TypeOfFuel/{id}");
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

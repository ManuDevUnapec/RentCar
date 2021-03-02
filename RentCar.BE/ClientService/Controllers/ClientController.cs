using System;
using System.Threading.Tasks;
using ClientService.Core.Entities;
using ClientService.Core.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ClientService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;

        public ClientController(IClientRepository repository, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var clients = await _repository.GetAll();
                return Ok(clients);
            }catch(Exception e)
            {
                //Log error
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var client = await _repository.Get(id);
                return Ok(client);
            }catch(Exception e)
            {
                //Log error
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpGet("GetReport")]
        public async Task<IActionResult> GetReport(int? id = null, string name = null, string identificationCard = null,
            string cardNumber = null, int? creditLimit = null, string personType = null, string status = null)
        {
            try
            {
                var cars = await _repository.GetReport(id, name, identificationCard, cardNumber, creditLimit, personType,
                    status);
                return Ok(cars);
            }catch(Exception e)
            {
                //Log error
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Client client)
        {
            try
            {
                var response = await _repository.Add(client);
                if (response != 0)
                {
                    return Ok("Added successfully");
                }
                else
                {
                    return BadRequest("An error ocurred, contact IT Staff");
                }
            }catch(Exception e)
            {
                //Log error
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Client client)
        {
            try
            {
                var response = await _repository.Update(client);
                if (response != 0)
                {
                    //Sending the object to the client exchange
                    await _publishEndpoint.Publish<Client>(client);

                    return Ok("Updated successfully");
                }
                else
                {
                    return BadRequest("An error ocurred, contact IT Staff");
                }
            }catch(Exception e)
            {
                //Log error
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _repository.Delete(id);
                if (response != 0)
                {
                    return Ok("Deleted successfully");
                }
                else
                {
                    return BadRequest("An error ocurred, contact IT Staff");
                }
            }catch(Exception e)
            {
                //Log error
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }
    }
}

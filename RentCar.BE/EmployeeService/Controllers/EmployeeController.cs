using System;
using System.Threading.Tasks;
using EmployeeService.Core.Entities;
using EmployeeService.Core.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EmployeeService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
     
        private readonly IEmployeeRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;

        public EmployeeController(IEmployeeRepository repository, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var employees = await _repository.GetAll();
                return Ok(employees);
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
                var employee = await _repository.Get(id);
                return Ok(employee);
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
            string hourHand = null, string commisionPercent = null, string status = null)
        {
            try
            {
                var employees = await _repository.GetReport(id, name, identificationCard, hourHand, commisionPercent,
                    status);
                return Ok(employees);
            }catch(Exception e)
            {
                //Log error
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Employee employee)
        {
            try
            {
                var response = await _repository.Add(employee);
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
        public async Task<IActionResult> Put(Employee employee)
        {
            try
            {
                var response = await _repository.Update(employee);
                if (response != 0)
                {
                    //Sending the object to the employee exchange
                    await _publishEndpoint.Publish<Employee>(employee);

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

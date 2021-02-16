using System;
using System.Threading.Tasks;
using EmployeeService.Core.Entities;
using EmployeeService.Core.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

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
            var employees = await _repository.GetAll();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var employee = await _repository.Get(id);
            return Ok(employee);
        }

        [HttpGet("GetReport")]
        public async Task<IActionResult> GetReport(int? id = null, string name = null, string identificationCard = null,
            string hourHand = null, string commisionPercent = null, DateTime? created = null, string status = null)
        {
            var employees = await _repository.GetReport(id, name, identificationCard, hourHand, commisionPercent,
                created, status);
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Employee employee)
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
        }

        [HttpPut]
        public async Task<IActionResult> Put(Employee employee)
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
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
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
        }
    }
}

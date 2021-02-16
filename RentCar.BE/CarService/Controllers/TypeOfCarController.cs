using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarService.Core.Entities;
using CarService.Core.Interfaces;

namespace CarService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeOfCarController : ControllerBase
    {
        private readonly ITypeOfCarRepository _repository;

        public TypeOfCarController(ITypeOfCarRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var typeOfCars = await _repository.GetAll();
            return Ok(typeOfCars);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var typeOfCar = await _repository.Get(id);
            return Ok(typeOfCar);
        }

        [HttpGet("GetReport")]
        public async Task<IActionResult> GetReport(int? id = null, string description = null, string status = null)
        {
            var typeOfCars = await _repository.GetReport(id, description, status);
            return Ok(typeOfCars);
        }

        [HttpPost]
        public async Task<IActionResult> Post(TypeOfCar typeOfCar)
        {
            var response = await _repository.Add(typeOfCar);
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
        public async Task<IActionResult> Put(TypeOfCar typeOfCar)
        {
            var response = await _repository.Update(typeOfCar);
            if (response != 0)
            {
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

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentCar.Core.Entities;
using RentCar.Core.Interfaces;

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

        [HttpGet("GetByID/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var typeOfCar = await _repository.Get(id);
            return Ok(typeOfCar);
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

        [HttpDelete]
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

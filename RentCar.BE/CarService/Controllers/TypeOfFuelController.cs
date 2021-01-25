using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarService.Core.Entities;
using CarService.Core.Interfaces;

namespace CarService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeOfFuelController : ControllerBase
    {
        private readonly ITypeOfFuelRepository _repository;

        public TypeOfFuelController(ITypeOfFuelRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var typeOfFuels = await _repository.GetAll();
            return Ok(typeOfFuels);
        }

        [HttpGet("GetByID/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var typeOfFuel = await _repository.Get(id);
            return Ok(typeOfFuel);
        }

        [HttpPost]
        public async Task<IActionResult> Post(TypeOfFuel typeOfFuel)
        {
            var response = await _repository.Add(typeOfFuel);
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
        public async Task<IActionResult> Put(TypeOfFuel typeOfFuel)
        {
            var response = await _repository.Update(typeOfFuel);
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

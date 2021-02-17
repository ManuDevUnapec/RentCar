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
            try
            {
                var typeOfCars = await _repository.GetAll();
                return Ok(typeOfCars);
            }
            catch (Exception e)
            {
                //Log e
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var typeOfCar = await _repository.Get(id);
                return Ok(typeOfCar);
            }
            catch (Exception e)
            {
                //Log e
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpGet("GetReport")]
        public async Task<IActionResult> GetReport(int? id = null, string description = null, string status = null)
        {
            try
            {
                var typeOfCars = await _repository.GetReport(id, description, status);
                return Ok(typeOfCars);
            }
            catch (Exception e)
            {
                //Log e
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(TypeOfCar typeOfCar)
        {
            try
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
            catch (Exception e)
            {
                //Log e
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(TypeOfCar typeOfCar)
        {
            try
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
            catch (Exception e)
            {
                //Log e
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
            }
            catch (Exception e)
            {
                //Log e
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }
    }
}

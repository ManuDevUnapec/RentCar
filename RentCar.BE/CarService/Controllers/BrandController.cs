using System;
using System.Threading.Tasks;
using CarService.Core.Entities;
using CarService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository _repository;

        public BrandController(IBrandRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var brands = await _repository.GetAll();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var brand = await _repository.Get(id);
            return Ok(brand);
        }

        [HttpGet("GetReport/{id?}/{description?}/{status:?}")]
        public async Task<IActionResult> GetReport(int? id = null, string description = null, string status = null)
        {
            var brand = await _repository.GetReport(id, description, status);
            return Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Brand brand)
        {
            var response = await _repository.Add(brand);
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
        public async Task<IActionResult> Put(Brand brand)
        {
            var response = await _repository.Update(brand);
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

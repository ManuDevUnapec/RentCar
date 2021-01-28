using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentService.Core.Entities;
using RentService.Core.Interfaces;

namespace RentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentController : ControllerBase
    {
        private readonly IRentRepository _repository;

        public RentController(IRentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rents = await _repository.GetAll();
            return Ok(rents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var rent = await _repository.Get(id);
            return Ok(rent);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Rent rent)
        {
            var response = await _repository.Add(rent);
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
        public async Task<IActionResult> Put(Rent rent)
        {
            var response = await _repository.Update(rent);
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

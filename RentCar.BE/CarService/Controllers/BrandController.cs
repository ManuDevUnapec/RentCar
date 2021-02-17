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
            try
            {
                var brands = await _repository.GetAll();
                return Ok(brands);
            }catch(Exception e)
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
                var brand = await _repository.Get(id);
                return Ok(brand);
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
                var brands = await _repository.GetReport(id, description, status);
                return Ok(brands);
            }
            catch (Exception e)
            {
                //Log e
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Brand brand)
        {
            try
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
            catch (Exception e)
            {
                //Log e
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Brand brand)
        {
            try
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

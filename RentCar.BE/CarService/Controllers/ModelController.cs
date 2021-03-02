using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarService.Core.Entities;
using CarService.Core.Interfaces;
using Serilog;

namespace CarService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModelController : ControllerBase
    {
        private readonly IModelRepository _repository;

        public ModelController(IModelRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var models = await _repository.GetAll();
                return Ok(models);
            }
            catch (Exception e)
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
                var model = await _repository.Get(id);
                return Ok(model);
            }
            catch (Exception e)
            {
                //Log error
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpGet("GetReport")]
        public async Task<IActionResult> GetReport(int? id = null, string description = null, string status = null,
            int? brandID = null)
        {
            try
            {
                var models = await _repository.GetReport(id, description, status, brandID);
                return Ok(models);
            }
            catch (Exception e)
            {
                //Log error
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Model model)
        {
            try
            {
                var response = await _repository.Add(model);
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
                //Log error
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Model model)
        {
            try
            {
                var response = await _repository.Update(model);
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
            }
            catch (Exception e)
            {
                //Log error
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }
    }
}

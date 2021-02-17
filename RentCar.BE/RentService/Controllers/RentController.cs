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
            try
            {
                var rents = await _repository.GetAll();
                return Ok(rents);
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
                var rent = await _repository.Get(id);
                return Ok(rent);
            }catch(Exception e)
            {
                //Log e
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpGet("GetReport")]
        public async Task<IActionResult> GetReport(int? id = null, int? amountForDays = null, 
            int? numberOfDays = null, string status = null, int? employeeID = null, int? clientID = null,
            int? carID = null)
        {
            try
            {
                var cars = await _repository.GetReport(id, amountForDays, numberOfDays,
                    status, employeeID, clientID, carID);
                return Ok(cars);
            }catch(Exception e)
            {
                //Log e
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Rent rent)
        {
            try
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
            catch (Exception e)
            {
                //Log e
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Rent rent)
        {
            try
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

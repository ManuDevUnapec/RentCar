using System;
using System.Threading.Tasks;
using InspectionService.Core.Entities;
using InspectionService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InspectionService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InspectionController : ControllerBase
    {
        private readonly IInspectionRepository _repository;

        public InspectionController(IInspectionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var inspections = await _repository.GetAll();
                return Ok(inspections);
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
                var inspection = await _repository.Get(id);
                return Ok(inspection);
            }
            catch (Exception e)
            {
                //Log e
                return BadRequest("An error ocurred, contact IT Staff");
            }
            
        }

        [HttpGet("GetReport")]
        public async Task<IActionResult> GetReport(int? id = null, bool? grated = null, bool? cat = null,
            bool? rubberBack = null, bool? glassBreak = null, bool? rubberStateOne = null,
            bool? rubberStateTwo = null, bool? rubberStateThree = null, bool? rubberStateFourth = null,
            string amountOfFuel = null, string status = null, int? employeeID = null, int? clientID = null,
            int? carID = null)
        {
            try
            {
                var cars = await _repository.GetReport(id, grated, cat, rubberBack, glassBreak,
                    rubberStateOne, rubberStateTwo, rubberStateThree, rubberStateFourth, amountOfFuel, status,
                    employeeID, clientID, carID);
                return Ok(cars);
            }
            catch (Exception e)
            {
                //Log e
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Inspection inspection)
        {
            try
            {
                var response = await _repository.Add(inspection);
                if (response != 0)
                {
                    return Ok("Added successfully");
                }
                else
                {
                    return BadRequest("An error ocurred, contact IT Staff");
                }
            }catch(Exception e)
            {
                //Log e
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Inspection inspection)
        {
            try
            {
                var response = await _repository.Update(inspection);
                if (response != 0)
                {
                    return Ok("Updated successfully");
                }
                else
                {
                    return BadRequest("An error ocurred, contact IT Staff");
                }
            }catch(Exception e)
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
            }catch(Exception e)
            {
                //Log e
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }
    }
}

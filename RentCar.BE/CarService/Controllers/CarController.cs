﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarService.Core.Entities;
using CarService.Core.Interfaces;

namespace CarService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _repository;

        public CarController(ICarRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cars = await _repository.GetAll();
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var car = await _repository.Get(id);
            return Ok(car);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Car car)
        {
            var response = await _repository.Add(car);
            if(response != 0)
            {
                return Ok("Added successfully");
            }
            else
            {
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Car car)
        {
            var response = await _repository.Update(car);
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

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCarWorkship.Models.Db;
using RentCarWorkship.Models.Dto;
using RentCarWorkship.Repository.Interface;
using RentCarWorkship.Services.Interface;

namespace RentCarWorkship.Controllers;

[ApiController]
[Route("Car")]
public class CarController : BaseController
{
    private readonly ICarService _carService;

    public CarController(ICarService carService)
    {
        this._carService = _carService;
    }
    [AllowAnonymous]
    [HttpGet("id")]
    public async Task<DbCar> GetCar(int id)
    {
        return await _carService.GetInfoById(id);
    }
    

    [HttpPost]
    public async Task<IActionResult> AddCar(DbCar car)
    {
        if (await _carService.CheckCar(car.CarId))
        {
            return BadRequest("car already exist");
        }
        else
        {
            await _carService.AddCar(car);
            return Ok("car was added");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCar(UpdateCarDto car)
    {
        if (await _carService.CheckCar(car.id))
        {
            _carService.UpdateCar(car);
            return Ok("car was updated");
        }

        return BadRequest("car with that id doesn`t exist");
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteCar(int id)
    {
        if (await _carService.CheckCar(id))
        {
            if (Id != await _carService.GetUserById(id))
            {
                return Unauthorized();
            }
            _carService.DeleteCar(id);
            return Ok("car have been deleted");
        }
        else
        {
            return BadRequest("car with that id doesn`t exist");
        }
    }
}
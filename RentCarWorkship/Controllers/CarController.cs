using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCarWorkship.Models.Db;
using RentCarWorkship.Models.Dto;
using RentCarWorkship.Repository.Interface;

namespace RentCarWorkship.Controllers;

[ApiController]
[Route("Car")]
public class CarController : BaseController
{
    private readonly ICarRepository _carRepository;

    public CarController(ICarRepository carRepository)
    {
        this._carRepository = carRepository;
    }

    [HttpGet("id")]
    public async Task<DbCar> GetCar(int id)
    {
        return await _carRepository.GetInfoById(id);
    }

    [HttpPut]
    public async Task<IActionResult> AddCar(DbCar car)
    {
        if (await _carRepository.CheckCar(car.CarId))
        {
            return BadRequest("car already exist")
        }
        else
        {
            await _carRepository.AddCar(car);
            return Ok("car was added");
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCar(UpdateCarDto car)
    {
        if (await _carRepository.CheckCar(car.id))
        {
            _carRepository.UpdateCar(car);
            return Ok("car was updated");
        }

        return BadRequest("car with that id doesn`t exist");
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteCar(int id)
    {
        if (await _carRepository.CheckCar(id))
        {
            var rawUserId = HttpContext.User.FindFirstValue("id");
            if (int.TryParse(rawUserId, out var UserId))
            {
                if (UserId != await _carRepository.GetUserById(id))
                {
                    return Unauthorized();
                }
            }
            _carRepository.DeleteCar(id);
            return Ok("car have been deleted");
        }
        else
        {
            return BadRequest("car with that id doesn`t exist");
        }
    }
}
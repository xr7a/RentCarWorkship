using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCarWorkship.Models.Db;
using RentCarWorkship.Repository;
using RentCarWorkship.Services.Interface;

namespace RentCarWorkship.Controllers;

[ApiController]
[Route("Rent")]
public class RentController : BaseController
{
    private readonly IRentService _rentService;

    public RentController(IRentService rentService)
    {
        this._rentService = rentService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetCarsForRent(Rent search)
    {
        if (await _rentService.CheckCarForRent(search))
        {
            return Ok(_rentService.GetCarsForRent(search));
        }

        return BadRequest("cars weren`t found");
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetCarInfo(int id)
    {
        if(await )
    }



}
using Microsoft.AspNetCore.Mvc;
using RentCarWorkship.Models.Dto;

namespace RentCarWorkship.Controllers;

[ApiController]
[Route("account")]
public class AccountController: BaseController
{
    [HttpGet("me")]
    public IActionResult GetMe()
    {
        return Ok(new UserDataDto()
        {
            Id = Id,
            Email = Email,
            Username = Username,
            Role = Role
        });
    }
}
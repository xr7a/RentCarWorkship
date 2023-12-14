using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCarWorkship.Common;
using RentCarWorkship.Models.Db;
using RentCarWorkship.Models.Dto;
using RentCarWorkship.Services.Interface;

namespace RentCarWorkship.Controllers;

[ApiController]
[Route("Admin/Account")]
[Authorize(Roles = "admin")]
public class AdminAccountController : BaseController
{
    private readonly IAdminService _adminService;
    private readonly IJwtService _jwtService;

    public AdminAccountController(IAdminService adminService, IJwtService jwtService)
    {
        this._adminService = adminService;
        this._jwtService = jwtService;
    }
    // public async Task<IActionResult> CheckRole()
    // {
    //     if (Role == "admin")
    //     {
    //         return Ok();
    //     }
    //
    //     return Unauthorized();
    // }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // await CheckRole();
        return Ok(_adminService.AllUsers());
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetUserById(int id)
    {
        // await CheckRole();
        if (await _adminService.CheckAccountById(id))
        {
            return Ok(_adminService.GetUserById(id));
        }

        return BadRequest("account with that id doesn`t exist");
    }
    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] AdminRegisterDto req)
    {
        // await CheckRole();
        var refreshToken = _jwtService.CreateToken(new List<Claim>(), 72);
        if (await _jwtService.CheckAccount(req.Username))
        {
            return BadRequest("account had been already registered");
        }

        var id = await _adminService.CreateAccount(new DbAccount()
        {
            Username = req.Username,
            Password = req.Password,
            Role = req.Role,
            Balance = req.Balance,
            RefreshToken = refreshToken,
            RefreshTokenExpiredTime = DateTime.UtcNow.Add(TimeSpan.FromHours(72))

        });
        var claims = Jwt.GetClaims(id, req.Username, "user");
        var accessToken = _jwtService.CreateToken(claims, 1);
        return Ok(new AuthDtoResponse()
        {
            RefreshToken = refreshToken,
            AccessToken = accessToken
        });
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAccount(int id)
    {
        
    }

}
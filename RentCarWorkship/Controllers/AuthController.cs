using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RentCarWorkship.Common;
using RentCarWorkship.Models.Db;
using RentCarWorkship.Services.Interface;
using RentCarWorkship.Models.Dto;

namespace RentCarWorkship.Controllers;

[Route("auth")]
public class AuthController: Controller
{
    private readonly IAccountService _accountService;
    private readonly IJwtService _jwtService;

    public AuthController(IAccountService accountService, IJwtService jwtService)
    {
        this._accountService = accountService;
        this._jwtService = jwtService;

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto req)
    {
        var account = await _accountService.GetAccountData(req.Email, req.Password);
        if (account == null)
        {
            return BadRequest("account doesn`t exist");
        }

        var claims = Jwt.GetClaims(account.Id, req.Email, account.Username, account.Role);
        var accessToken = _jwtService.CreateToken(claims, 1);
        return Ok(new AuthDtoResponse());
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto req)
    {
        var refreshToken = _jwtService.CreateToken(new List<Claim>(), 72);
        if (await _jwtService.CheckAccount(req.Email))
        {
            return BadRequest("account had been already registered");
        }

        var id = await _accountService.CreateAccount(new DbAccount()
        {
            Email = req.Email,
            Username = req.Username,
            Password = req.Password,
            Role = req.Role,
            RefreshToken = refreshToken,
            RefreshTokenExpiredTime = DateTime.UtcNow.Add(TimeSpan.FromHours(72))
                
        });
        var claims = Jwt.GetClaims(id, req.Email, req.Username, req.Role);
        var accessToken = _jwtService.CreateToken(claims, 1);
        return Ok(new AuthDtoResponse()
        {
            RefreshToken = refreshToken,
            AccessToken = accessToken
        });
    }
    [HttpPut("expire-token/{token}")]
    public async Task<IActionResult> ExpireToken([FromRoute] string token)
    {
        return Ok(await _jwtService.ExpireToken(token));
    }
}
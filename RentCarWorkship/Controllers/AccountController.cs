using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCarWorkship.Common;
using RentCarWorkship.Models.Dto;
using RentCarWorkship.Services.Interface;
using RentCarWorkship.Models.Db;
namespace RentCarWorkship.Controllers;

[ApiController]
[Route("Account")]
public class AccountController: BaseController
{
    private readonly IAccountService _accountService;
    private readonly IJwtService _jwtService;

    public AccountController(IAccountService accountService, IJwtService jwtService)
    {
        this._accountService = accountService;
        this._jwtService = jwtService;

    }

    [HttpGet("me")]
    public IActionResult GetMe()
    {
        return Ok(new UserDataDto()
        {
            Id = Id,
            Username = Username,
            Role = Role
        });
    }
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto req)
    {
        var account = await _accountService.GetAccountData(req.Username, req.Password);
        if (account == null)
        {
            return BadRequest("account doesn`t exist");
        }

        var claims = Jwt.GetClaims(account.Id, account.Username, account.Role);
        var accessToken = _jwtService.CreateToken(claims, 1);
        return Ok(new AuthDtoResponse());
    }
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto req)
    {
        var refreshToken = _jwtService.CreateToken(new List<Claim>(), 72);
        if (await _jwtService.CheckAccount(req.Username))
        {
            return BadRequest("account had been already registered");
        }

        var id = await _accountService.CreateAccount(new DbAccount()
        {
            Username = req.Username,
            Password = req.Password,
            Role = req.Role,
            RefreshToken = refreshToken,
            RefreshTokenExpiredTime = DateTime.UtcNow.Add(TimeSpan.FromHours(72))
                
        });
        var claims = Jwt.GetClaims(id, req.Username, req.Role);
        var accessToken = _jwtService.CreateToken(claims, 1);
        return Ok(new AuthDtoResponse()
        {
            RefreshToken = refreshToken,
            AccessToken = accessToken
        });
    }

    [Authorize]
    [HttpPut("logout")]
    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok();
    }
    [HttpPut("expire-token/{token}")]
    public async Task<IActionResult> ExpireToken([FromRoute] string token)
    {
        return Ok(await _jwtService.ExpireToken(token));
    }
}

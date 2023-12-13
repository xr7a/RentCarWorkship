using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCarWorkship.Common;

namespace RentCarWorkship.Controllers;

[Authorize]
[ApiController]
public class BaseController: ControllerBase
{
    private string AuthHeader => HttpContext.Request.Headers["Authorization"].ToString();

    protected int Id => Convert.ToInt32(Jwt.GetId(AuthHeader));
    protected string? Username => Jwt.GetUsername(AuthHeader);
    protected string? Role => Jwt.GetRole(AuthHeader);
}
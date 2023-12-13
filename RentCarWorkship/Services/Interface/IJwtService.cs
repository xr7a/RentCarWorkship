using System.Security.Claims;
using RentCarWorkship.Models.Dto;
namespace RentCarWorkship.Services.Interface;

public interface IJwtService
{
    string CreateToken(ICollection<Claim> claims, int tokenExpiresAfterHours = 0);
    Task<bool> CheckAccount(string username);
    Task GetTokenById(int id);
    Task<AuthDtoResponse> ExpireToken(string token);
}
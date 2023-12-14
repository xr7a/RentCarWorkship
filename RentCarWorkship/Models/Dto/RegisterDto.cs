using System.Text.Json.Serialization;

namespace RentCarWorkship.Models.Dto;

public class RegisterDto
{
    [JsonRequired] public string? Username { get; set; }
    [JsonRequired] public string? Password { get; set; }
    
}
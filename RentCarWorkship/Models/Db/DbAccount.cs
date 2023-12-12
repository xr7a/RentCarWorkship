namespace RentCarWorkship.Models.Db;

public class DbAccount
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiredTime { get; set; }
}
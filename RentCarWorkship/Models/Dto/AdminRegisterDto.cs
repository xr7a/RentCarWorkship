namespace RentCarWorkship.Models.Dto;

public class AdminRegisterDto: RegisterDto
{
    public string Role { get; set; }
    public double Balance { get; set; }
}
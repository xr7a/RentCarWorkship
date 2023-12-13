namespace RentCarWorkship.Models.Dto;

public class UpdateCarDto
{
    public int id { get; set; }
    public bool CanBeRented { get; set; }
    public string model { get; set; }
    public string color { get; set; }
    public string identifier { get; set; }
    public string description { get; set; }
    public double latitude { get; set; }
    public double longitude { get; set; }
    public double minutePrice { get; set; }
    public double dayPrice { get; set; }
}
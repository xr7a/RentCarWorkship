namespace RentCarWorkship.Models.Db;

public class DbCar
{
    public int CarId { get; set; }
    public bool CanBeRented { get; set; }
    public string TransportType { get; set; }
    public string model { get; set; }
    public string color { get; set; }
    public string identtifier { get; set; }
    public string? description { get; set; }
    public double latitude { get; set; }
    public double longitude { get; set; }
    public double? minutePrice { get; set; }
    public double? dayPrice { get; set; }
}
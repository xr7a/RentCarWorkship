using RentCarWorkship.Models.Db;

namespace RentCarWorkship.Services.Interface;

public interface IRentService
{
    Task<bool> CheckCarForRent(Rent search);
    List<DbCar> GetCarsForRent(Rent search);
}
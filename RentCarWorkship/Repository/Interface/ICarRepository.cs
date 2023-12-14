using RentCarWorkship.Models.Db;
using RentCarWorkship.Models.Dto;

namespace RentCarWorkship.Repository.Interface;

public interface ICarRepository
{
    Task<bool> CheckCar(int id);
    Task<DbCar> GetInfoById(int id);
    Task<int> GetUserById(int id);
    Task<int> AddCar(DbCar car);
    void UpdateCar(UpdateCarDto car);
    void DeleteCar(int id);
    Task<bool> CheckCarForRent(Rent search);
    List<DbCar> GetCarsForRent(Rent search);
}
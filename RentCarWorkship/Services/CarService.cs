using RentCarWorkship.Models.Db;
using RentCarWorkship.Models.Dto;
using RentCarWorkship.Repository;
using RentCarWorkship.Services.Interface;

namespace RentCarWorkship.Services;

public class CarService : ICarService
{
    private readonly CarRepository _carRepository;

    public CarService(CarRepository carRepository)
    {
        this._carRepository = carRepository;
    }

    public async Task<bool> CheckCar(int id)
    {
        return await _carRepository.CheckCar(id);
    }

    public async Task<DbCar> GetInfoById(int id)
    {
        return await _carRepository.GetInfoById(id);
    }

    public async Task<int> GetUserById(int id)
    {
        return await _carRepository.GetUserById(id);
    }

    public async Task<int> AddCar(DbCar car)
    {
        return await _carRepository.AddCar(car);
    }

    public void UpdateCar(UpdateCarDto car)
    {
        _carRepository.UpdateCar(car);
    }

    public void DeleteCar(int id)
    {
        _carRepository.DeleteCar(id);
    }
}
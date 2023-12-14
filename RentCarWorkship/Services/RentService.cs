using RentCarWorkship.Models.Db;
using RentCarWorkship.Repository;
using RentCarWorkship.Services.Interface;

namespace RentCarWorkship.Services;

public class RentService: IRentService
{
    private readonly CarRepository _carRepository;

    public RentService(CarRepository carRepository)
    {
        this._carRepository = carRepository;
    }

    public async Task<bool> CheckCarForRent(Rent search)
    {
        return await _carRepository.CheckCarForRent(search);
    }

    public List<DbCar> GetCarsForRent(Rent search)
    {
        return _carRepository.GetCarsForRent(search);
    } 
    
}
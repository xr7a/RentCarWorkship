using System.Data.Common;
using System.Reflection.Metadata;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RentCarWorkship.Models.Db;
using RentCarWorkship.Models.Dto;
using RentCarWorkship.Repository.Interface;


namespace RentCarWorkship.Repository;

public class CarRepository: ICarRepository
{
    private readonly DbConnection connection;

    public CarRepository(DbConnection _connection)
    {
        this.connection = _connection;
    }


    public async Task<bool> CheckCar(int id)
    {
        return await connection.QueryFirstOrDefaultAsync($@"select * from cars where CarId = {id}") != null;
    }
    public async Task<DbCar> GetInfoById(int id)
    {
        return await connection.QueryFirstOrDefaultAsync(
            @$"select * from cars where id = {id} ");
    }

    public async Task<int> GetUserById(int id)
    {
        return await connection.QueryFirstOrDefaultAsync($@"select UserId from cars where id = {id}");
    }

    public async Task<int> AddCar(DbCar car)
    {
        return await connection.ExecuteAsync(
            @$"insert into cars (CarId, CanBeRented, TrasportType, model, color, identifier,
                  description, latitude, longtitude, minutePrice, dayPrice) values 
                  '{car.CarId}',
                   '{car.CanBeRented}',                                                         
                   '{car.TransportType}',                                                         
                   '{car.model}',                                                         
                   '{car.color}',                                                         
                   '{car.identtifier}',                                                         
                   '{car.description}',                                                         
                   '{car.latitude}',                                                         
                   '{car.longitude}',                                                         
                   '{car.minutePrice}',                                                         
                   '{car.dayPrice}',
                    returning CarId");
    }

    public void UpdateCar(UpdateCarDto car)
    {
        connection.ExecuteAsync($@"update cars set 
                CanBeRented = '{car.CanBeRented}',
                model = '{car.model}',
                color = '{car.color}',
                Identifier = '{car.identifier}',
                description = '{car.description}',
                latitude = '{car.latitude}',
                longitude = '{car.longitude}',
                minutePrice = '{car.minutePrice}',
                dayPrice = '{car.dayPrice}',
                ");
    }

    public void DeleteCar(int id)
    {
        connection.ExecuteAsync($@"delete from cars where CarId = {id}");
    }

    public async Task<bool> CheckCarForRent(Rent search)
    {
        return await connection.QueryFirstOrDefaultAsync(@$"select * from cars where latitude <= {search.lat + search.radius} and 
                         longitude = {search.longs + search.radius} and TransportType = {search.type} ") != null;
    }
    public List<DbCar> GetCarsForRent(Rent search)
    {
        return connection.Query<DbCar>(@$"select * from cars where CanBeRented = {1} latitude <= {search.lat + search.radius} and 
                         longitude = {search.longs + search.radius} and TransportType = {search.type} ").ToList();
    }
    public async Task<DbCar> GetCarById(int id)
    {
        return await connection.QueryFirstOrDefaultAsync($@"select model, color, minutePrice, dayPrice from cars
        where id = {id}");
    }
    
}
using RentCarWorkship.Models.Db;

namespace RentCarWorkship.Services.Interface;

public interface IAdminService
{
    List<DbAccount> AllUsers();
    Task<DbAccount> GetUserById(int id);
    Task<bool> CheckAccountById(int id);
    Task<int> CreateAccount(DbAccount account);
    
}
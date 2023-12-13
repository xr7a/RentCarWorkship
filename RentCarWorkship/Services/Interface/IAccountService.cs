using RentCarWorkship.Models.Db;

namespace RentCarWorkship.Services.Interface;

public interface IAccountService
{
    Task<int> CreateAccount(DbAccount account);
    Task<DbAccount> GetAccountData(string username, string password);
}
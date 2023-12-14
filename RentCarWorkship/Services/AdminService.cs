using RentCarWorkship.Models.Db;
using RentCarWorkship.Repository;
using RentCarWorkship.Services.Interface;

namespace RentCarWorkship.Services;

public class AdminService: IAdminService
{
    private readonly AccountRepository _accountRepository;

    public AdminService(AccountRepository accountRepository)
    {
        this._accountRepository = accountRepository;
    }
    public List<DbAccount> AllUsers()
    {
        return _accountRepository.AllUsers();
    }

    public async Task<bool> CheckAccountById(int id)
    {
        return await _accountRepository.CheckAccountById(id);
    }

    public async Task<DbAccount> GetUserById(int id)
    {
        return await _accountRepository.GetAccount(id);
    }
    public async Task<int> CreateAccount(DbAccount account)
    {
        return await _accountRepository.CreateAccount(account);
    }

}
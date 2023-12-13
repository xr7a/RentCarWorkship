using Microsoft.VisualBasic;
using RentCarWorkship.Models.Db;
using RentCarWorkship.Repository.Interface;
using RentCarWorkship.Services.Interface;

namespace RentCarWorkship.Services;

public class AccountService: IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        this._accountRepository = accountRepository;
    }
    public async Task<int> CreateAccount(DbAccount account)
    {
        return await _accountRepository.CreateAccount(account);
    }

    public async Task<DbAccount> GetAccountData(string username, string password)
    {
        return await _accountRepository.GetAccountData(username, password);
    }
}
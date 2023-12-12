using System.Data;
using RentCarWorkship.Repository.Interface;
using Dapper;
using RentCarWorkship.Common;
using RentCarWorkship.Models.Db;

namespace RentCarWorkship.Repository;

public class AccountRepository: IAccountRepository
{
    private readonly IDbConnection connection;

    public AccountRepository(IDbConnection connection)
    {
        this.connection = connection;
    }

    public async Task<bool> CheckAccount(string email){
    
            return await connection.QueryFirstOrDefaultAsync<DbAccount>(
                $@"select id from accounts where email = '{email}' ") != null;
    }

    [Obsolete("Obsolete")]
    public async Task<int> CreateAccount(DbAccount user)
    {
            var id = await connection.ExecuteAsync(
                @$"insert into accounts (email, password, username, role, refresh_token, refresh_token_expired_time)
                values ('{user.Email}',
                        '{Hash.GetHash(user.Password)}', 
                        '{user.Username}', 
                        '{user.Role}',
                        '{user.RefreshToken}', 
                        '{user.RefreshTokenExpiredTime}')
                         returning id");

            return id;
    }

    [Obsolete("Obsolete")]
    public async Task<DbAccount?> GetAccountData(string email, string password)
    {
            return await connection.QueryFirstOrDefaultAsync<DbAccount>(
                @$"select id, role, refresh_token as RefreshToken, username 
                from accounts where email = '{email}' and 
                password='{Hash.GetHash(password)}'");
    }

    public async Task<bool> CheckRefreshToken(string token)
    {
        return await connection.QueryFirstOrDefaultAsync<string>(
            $@"select refresh_token from accounts where refresh_token = '{token}'") == null;
    }

    public async Task<DbAccount?> GetAccountDataByToken(string token)
    {
        return await connection.QueryFirstOrDefaultAsync<DbAccount>(
            $@"select id, email, username, role from accounts where refresh_token = '{token}'");
    }

    public async Task UpdateRefresh(string token, DateTime tokenTime)
    {
        await connection.ExecuteAsync($@"update accounts set 
                    refresh_token = '{token}', refresh_token_expired_time='{tokenTime}'");
    }
}
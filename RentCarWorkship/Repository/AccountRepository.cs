using System.Data;
using RentCarWorkship.Repository.Interface;
using Dapper;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
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
    
    public List<DbAccount> AllUsers()
    {
        return connection.Query<DbAccount>("select * from users").ToList();
    }

    public async Task<DbAccount> GetAccount(int id)
    {
        return await connection.QueryFirstOrDefaultAsync<DbAccount>($@"select * from accounts
        where id = {id}");
    }


    public async Task<bool> CheckAccount(string username){
    
            return await connection.QueryFirstOrDefaultAsync<DbAccount>(
                $@"select id from accounts where username = '{username}' ") != null;
    }public async Task<bool> CheckAccountById(int id){
    
            return await connection.QueryFirstOrDefaultAsync<DbAccount>(
                $@"select id from accounts where id = '{id}' ") != null;
    }

    [Obsolete("Obsolete")]
    public async Task<int> CreateAccount(DbAccount user)
    {
            var id = await connection.ExecuteAsync(
                @$"insert into accounts (username, password, role, refreshToken, refreshTokenExpiredTime)
                values ('{user.Username}',
                        '{Hash.GetHash(user.Password)}', 
                        '{user.Role}',
                        '{user.RefreshToken}', 
                        '{user.RefreshTokenExpiredTime}')
                         returning id");

            return id;
    }

    [Obsolete("Obsolete")]
    public async Task<DbAccount?> GetAccountData(string username, string password)
    {
            return await connection.QueryFirstOrDefaultAsync<DbAccount>(
                @$"select id, role, refreshToken as RefreshToken  
                from accounts where username = '{username}' and 
                password='{Hash.GetHash(password)}'");
    }

    public async Task<bool> CheckRefreshToken(string token)
    {
        return await connection.QueryFirstOrDefaultAsync<string>(
            $@"select refreshToken from accounts where refreshToken = '{token}'") == null;
    }

    public async Task<DbAccount?> GetAccountDataByToken(string token)
    {
        return await connection.QueryFirstOrDefaultAsync<DbAccount>(
            $@"select id, email, username, role from accounts where refreshToken = '{token}'");
    }

    public async Task UpdateRefresh(string token, DateTime tokenTime)
    {
        await connection.ExecuteAsync($@"update accounts set 
                    refreshToken = '{token}', refreshTokenExpiredTime='{tokenTime}'");
    }

    public async Task GetTokenById(int id)
    {
        await connection.QueryAsync($@" select refreshToken from accounts where id = {id}");
    }
   
}
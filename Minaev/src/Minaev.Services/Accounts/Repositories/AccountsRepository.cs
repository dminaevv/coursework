using EFConfigurator.Models;
using EFConfigurator.Repositories;
using Minaev.Domain.Accounts;
using Minaev.Services.Accounts.Converters;
using Minaev.Tools.Types;

namespace Minaev.Services.Accounts.Repositories;

public class AccountsRepository : IAccountsRepository
{
    private readonly IDbRepository _dbRepository;

    public AccountsRepository(IDbRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public void SaveAccount(Account account)
    {
        AccountDB? existUser = _dbRepository.FirstOrDefault<AccountDB>(u => u.Id == account.Id.Value);

        if (existUser is null)
        {
            _dbRepository.Add(account.ToDb());
        }
        else
        {
            existUser.Username = account.Username;
            existUser.Password = account.Password;
            existUser.IsAdmin = account.IsAdmin;
            existUser.Balance = account.Balance;
            existUser.ModifiedDateTimeUtc = account.ModifiedDateTimeUtc;
            existUser.IsRemoved = account.IsRemoved;
        }

        _dbRepository.SaveChanges();
    }

    public Account? GetAccount(ID id)
    {
        AccountDB? account = _dbRepository.FirstOrDefault<AccountDB>(account =>
            account.Id == id.Value
        );

        return account?.ToAccount();
    }

    public Account? GetAccountByUserName(String username)
    {
        AccountDB? account = _dbRepository.FirstOrDefault<AccountDB>(account =>
            account.Username == username
        );

        return account?.ToAccount();
    }

    public Account? GetAccount(String username, String password)
    {
        AccountDB? account = _dbRepository.FirstOrDefault<AccountDB>(account =>
            account.Username == username
            && account.Password == password
        );

        return account?.ToAccount();
    }

    public JwtToken? GetBannedToken(String token)
    {
        return _dbRepository.FirstOrDefault<JwtTokenDB>(t => t.Token == token)?.ToToken();
    }

    public void BanJwtToken(String token)
    {
        JwtTokenDB jwt = new(token);

        _dbRepository.Add<JwtTokenDB>(jwt);
        _dbRepository.SaveChanges();
    }
}

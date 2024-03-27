using Minaev.Domain.Accounts;
using Minaev.Domain.Services.Accounts;
using Minaev.Services.Accounts.Repositories;
using Minaev.Tools.Extensions;
using Minaev.Tools.Types;

namespace Minaev.Services.Accounts;

public class AccountsService : IAccountsService
{
    private readonly IAccountsRepository _accountsRepository;

    public AccountsService(IAccountsRepository accountsRepository)
    {
        _accountsRepository = accountsRepository;
    }

    public void SaveAccount(Account account)
    {
        _accountsRepository.SaveAccount(account);
    }

    public Account? GetAccount(ID id)
    {
        return _accountsRepository.GetAccount(id);
    }

    public Account? GetAccountByUserName(String username)
    {
        return _accountsRepository.GetAccountByUserName(username);
    }

    public Account? GetAccount(String username, String password)
    {
        return _accountsRepository.GetAccount(username, password);
    }

    public Account? SignIn(String username, String password)
    {
        String passwordHash = password.GetHash();
        return _accountsRepository.GetAccount(username, passwordHash);
    }

    public Result SignUp(String username, String password)
    {
        username = username.Trim();
        password = password.Trim();

        if (username.IsNullOrWhitespace()) return Result.Fail("Не указан логин");
        if (password.IsNullOrWhitespace()) return Result.Fail("Не указан пароль");

        Account? existUser = GetAccountByUserName(username.ToLower());
        if (existUser != null) return Result.Fail("Такой пользователь уже зарегистрирован");

        String passwordHash = password.GetHash();
        Account newAccount = Account.Create(username, passwordHash, isAdmin: false, balance: 0, ID.Robot());

        SaveAccount(newAccount);

        return Result.Success();
    }

    public AccountInfo? GetAccountInfo(ID userId)
    {
        Account? account = GetAccount(userId);
        if (account == null) return null;

        AccountInfo accountInfo = AccountInfo.Create(account);

        return accountInfo;
    }

    public Result Update(ID userId, String username, String password)
    {
        if (username.IsNullOrWhitespace()) return Result.Fail("Не указан логин");
        if (password.IsNullOrWhitespace()) return Result.Fail("Не указан пароль");

        Account? editedUser = GetAccount(userId);
        if (editedUser is null) throw new Exception($"Не удалось найти редактируемого пользователя с id {userId}");

        Account? existUser = editedUser.Username == username ? null : GetAccountByUserName(username);
        if (existUser != null) return Result.Fail("Такой пользователь уже зарегистрирован");

        String passwordHash = password.GetHash();
        editedUser.Update(username, passwordHash, editedUser.IsAdmin, editedUser.Balance, userId);

        SaveAccount(editedUser);

        return Result.Success();
    }


    public JwtToken? GetBannedToken(String token)
    {
        return _accountsRepository.GetBannedToken(token);
    }

    public void BanJwtToken(String token)
    {
        _accountsRepository.BanJwtToken(token);
    }
}
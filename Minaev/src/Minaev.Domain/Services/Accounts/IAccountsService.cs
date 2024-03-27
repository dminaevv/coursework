using Minaev.Domain.Accounts;
using Minaev.Tools.Types;

namespace Minaev.Domain.Services.Accounts;

public interface IAccountsService
{
    void SaveAccount(Account account);
    Account? GetAccount(ID id);
    Account? GetAccountByUserName(String username);
    Account? GetAccount(String username, String password);
    Account? SignIn(String username, String password);
    Result SignUp(String username, String password);
    AccountInfo? GetAccountInfo(ID userId);
    Result Update(ID userId, String username, String password);


    JwtToken? GetBannedToken(String token);
    void BanJwtToken(String token);
}
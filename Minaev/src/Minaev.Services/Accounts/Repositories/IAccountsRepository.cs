using Minaev.Domain.Accounts;
using Minaev.Tools.Types;

namespace Minaev.Services.Accounts.Repositories;

public interface IAccountsRepository
{
    void SaveAccount(Account account);
    Account? GetAccount(String username, String password);
    Account? GetAccount(ID id);
    Account? GetAccountByUserName(String username);


    JwtToken? GetBannedToken(String token);
    void BanJwtToken(String token);
}

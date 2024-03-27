using Minaev.Domain.Users;
using Minaev.Tools.Types;

namespace Minaev.Domain.Services.Accounts;

public interface IUsersService
{
    Account? GetUser(ID id);
    Account? GetUser(String username, String password);
    Account? SignIn(String username, String password);
    Result SignUp(String username, String password);
    UserInfo? GetUserInfo(ID userId);
    Result Update(ID userId, String username, String password);
}
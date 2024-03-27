using Minaev.Tools.Types;

namespace Minaev.WebAPI.Infrastructure;

public class SystemUser
{
    public ID Id { get; }
    public String Login { get; }
    public String Token { get; }
    public Boolean IsAdmin { get; }

    public SystemUser(ID id, String login, String token, Boolean isAdmin)
    {
        Id = id;
        Login = login;
        Token = token;
        IsAdmin = isAdmin;
    }
}

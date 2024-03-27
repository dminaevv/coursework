namespace Minaev.Domain.Users;

public class UserInfo
{
    public String Login { get; }
    public UserRole Role { get; }
    public DateTime CreatedDateTimeUtc { get; }
    public DateTime? ModifiedDateTimeUtc { get; }

    public UserInfo(String login, UserRole role, DateTime? modifiedDateTimeUtc, DateTime createdDateTimeUtc)
    {
        Login = login;
        Role = role;
        CreatedDateTimeUtc = createdDateTimeUtc;
        ModifiedDateTimeUtc = modifiedDateTimeUtc;
    }

    public static UserInfo Create(Account account)
    {
        return new UserInfo(account.Login, account.Role, account.ModifiedDateTimeUtc, account.CreatedDateTimeUtc);
    }
}


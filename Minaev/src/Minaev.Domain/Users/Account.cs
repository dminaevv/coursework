using Minaev.Tools.Types;

namespace Minaev.Domain.Users;

public class Account
{
    public ID Id { get; }
    public String Login { get; private set; }
    public String PasswordHash { get; private set; }
    public UserRole Role { get; }
    public Double Balance { get; }
    public DateTime CreatedDateTimeUtc { get; }
    public DateTime? ModifiedDateTimeUtc { get; }
    public Boolean IsRemoved { get; }

    public Account(
        ID id, String login, String passwordHash, UserRole role,
        Double balance, DateTime createdDateTimeUtc, DateTime? modifiedDateTimeUtc,
        Boolean isRemoved
    )
    {
        Id = id;
        Login = login;
        PasswordHash = passwordHash;
        Role = role;
        Balance = balance;
        CreatedDateTimeUtc = createdDateTimeUtc;
        ModifiedDateTimeUtc = modifiedDateTimeUtc;
        IsRemoved = isRemoved;
    }

    public static Account Create(String username, String passwordHash, UserRole role, Double balance)
    {
        return new Account(
            ID.New(), username, passwordHash, role, balance,
            createdDateTimeUtc: DateTime.UtcNow, modifiedDateTimeUtc: null,
            isRemoved: false
        );
    }

    public void UpdateUsername(String username)
    {
        Login = username;
    }

    public void UpdatePassword(String passwordHash)
    {
        PasswordHash = passwordHash;
    }
}


using Minaev.Tools.Types;

namespace Minaev.Domain.Accounts;

public class Account
{
    public ID Id { get; }
    public String Username { get; private set; }
    public String Password { get; private set; }
    public Boolean IsAdmin { get; private set; }
    public Double Balance { get; private set; }
    public DateTime CreatedDateTimeUtc { get; }
    public ID CreatedUserId { get; }
    public DateTime? ModifiedDateTimeUtc { get; private set; }
    public Int64? ModifiedUserId { get; private set; }
    public Boolean IsRemoved { get; }

    public Account(
        ID id, String username, String password, Boolean isAdmin,
        Double balance, DateTime createdDateTimeUtc, ID createdUserId, DateTime? modifiedDateTimeUtc,
        Int64? modifiedUserId, Boolean isRemoved
    )
    {
        Id = id;
        Username = username;
        Password = password;
        IsAdmin = isAdmin;
        Balance = balance;
        CreatedDateTimeUtc = createdDateTimeUtc;
        CreatedUserId = createdUserId;
        ModifiedDateTimeUtc = modifiedDateTimeUtc;
        ModifiedUserId = modifiedUserId;
        IsRemoved = isRemoved;
    }

    public static Account Create(String username, String passwordHash, Boolean isAdmin, Double balance, ID useId)
    {
        return new Account(
            ID.New(), username, passwordHash, isAdmin, balance,
            createdDateTimeUtc: DateTime.UtcNow, createdUserId: useId, modifiedDateTimeUtc: null,
            modifiedUserId: null, isRemoved: false
        );
    }

    public void Update(String username, String passwordHash, Boolean isAdmin, Double balance, ID modifiedUserId)
    {
        Username = username;
        Password = passwordHash;
        IsAdmin = isAdmin;
        Balance = Balance;
        ModifiedDateTimeUtc = DateTime.UtcNow;
        ModifiedUserId = modifiedUserId;
    }
}


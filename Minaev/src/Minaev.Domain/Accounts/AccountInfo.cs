namespace Minaev.Domain.Accounts;

public class AccountInfo
{
    public String Id { get; }
    public String Username { get; }
    public Double Balance { get; }
    public Boolean IsAdmin { get; }
    public DateTime CreatedDateTimeUtc { get; }
    public DateTime? ModifiedDateTimeUtc { get; }

    public AccountInfo(String id, String username, Double balance, Boolean isAdmin, DateTime? modifiedDateTimeUtc, DateTime createdDateTimeUtc)
    {
        Id = id;
        Username = username;
        Balance = balance;
        IsAdmin = isAdmin;
        CreatedDateTimeUtc = createdDateTimeUtc;
        ModifiedDateTimeUtc = modifiedDateTimeUtc;
    }

    public static AccountInfo Create(Account account)
    {
        return new AccountInfo(account.Id.Value.ToString(), account.Username, account.Balance, account.IsAdmin, account.ModifiedDateTimeUtc, account.CreatedDateTimeUtc);
    }
}


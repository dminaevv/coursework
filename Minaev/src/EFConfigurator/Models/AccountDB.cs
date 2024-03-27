namespace EFConfigurator.Models;

public class AccountDB
{
    public Int64 Id { get; set; }
    public String Username { get; set; }
    public String Password { get; set; }
    public Boolean IsAdmin { get; set; }
    public Double Balance { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; }
    public Int64 CreatedUserId { get; set; }
    public DateTime? ModifiedDateTimeUtc { get; set; }
    public Int64? ModifiedUserId { get; set; }
    public Boolean IsRemoved { get; set; }

    public AccountDB(
        Int64 id, String username, String password, Boolean isAdmin,
        Double balance, DateTime createdDateTimeUtc, Int64 createdUserId,
        DateTime? modifiedDateTimeUtc, Int64? modifiedUserId, Boolean isRemoved
    )
    {
        Id = id;
        Username = username;
        Password = password;
        IsAdmin = isAdmin;
        Balance = balance;
        CreatedDateTimeUtc = createdDateTimeUtc;
        CreatedUserId = createdUserId; ;
        ModifiedDateTimeUtc = modifiedDateTimeUtc;
        ModifiedUserId = modifiedUserId; ;
        IsRemoved = isRemoved;
    }
}


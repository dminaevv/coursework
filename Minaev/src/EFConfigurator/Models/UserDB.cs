using Minaev.Domain.Users;
using Minaev.Tools.Types;

namespace EFConfigurator.Models;

public class UserDB
{
    public ID Id { get; set; }
    public String Login { get; set; }
    public String PasswordHash { get; set; }
    public UserRole Role { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; }
    public DateTime? ModifiedDateTimeUtc { get; set; }
    public Boolean IsRemoved { get; set; }

    public UserDB(
        ID id, String login, String passwordHash, UserRole role,
        DateTime createdDateTimeUtc, DateTime? modifiedDateTimeUtc,
        Boolean isRemoved
    )
    {
        Id = id;
        Login = login;
        PasswordHash = passwordHash;
        Role = role;
        CreatedDateTimeUtc = createdDateTimeUtc;
        ModifiedDateTimeUtc = modifiedDateTimeUtc;
        IsRemoved = isRemoved;
    }
}


namespace Minaev.Domain.Users;

public class UserJwtToken
{
    public String Token { get; }
    public Int64 UserId { get; }
    public DateTimeOffset ExpirationDateTime { get; }
    public Boolean IsBanned { get; set; }
}
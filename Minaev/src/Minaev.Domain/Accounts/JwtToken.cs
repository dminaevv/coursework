namespace Minaev.Domain.Accounts;

public class JwtToken
{
    public String Token { get; }

    public JwtToken(String token)
    {
        Token = token;
    }
}
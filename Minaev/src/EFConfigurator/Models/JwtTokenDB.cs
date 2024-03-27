using System.ComponentModel.DataAnnotations;

namespace EFConfigurator.Models;

public class JwtTokenDB
{
    [Key]
    public String Token { get; set; }

    public JwtTokenDB(String token)
    {
        Token = token;
    }
}
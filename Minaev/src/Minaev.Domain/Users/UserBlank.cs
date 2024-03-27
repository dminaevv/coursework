using Minaev.Tools.Types;

namespace Minaev.Domain.Users;

public class UserBlank
{
    public String Username { get;  set;  }
    public String Password{ get;  set; }
    public Boolean IsAdmin { get;  set; }
    public Double Balance { get; set; }
}


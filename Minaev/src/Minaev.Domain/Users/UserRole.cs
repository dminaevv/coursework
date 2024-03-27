namespace Minaev.Domain.Users;

public enum UserRole
{
    Admin = 1, 
    Simple = 2
}

public static class UserRoleExtensions
{
    public static String ToStringValue(this UserRole role)
    {
        return nameof(role);
    }
}
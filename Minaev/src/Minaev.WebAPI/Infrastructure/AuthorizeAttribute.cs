using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Minaev.WebAPI.Infrastructure;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly String? _role;

    public AuthorizeAttribute(String role)
    {
        _role = role;
    }

    public AuthorizeAttribute() { }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        Boolean allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute>().Any();
        if (allowAnonymous) return;

        SystemUser? user = (SystemUser)context.HttpContext.Items["Account"];
        if (user is null)
        {
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            return;
        }

        if (_role is not null)
        {
            if (_role == "Admin" && !user.IsAdmin)
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}
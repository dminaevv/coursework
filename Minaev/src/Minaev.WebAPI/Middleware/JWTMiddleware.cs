using Microsoft.IdentityModel.Tokens;
using Minaev.Domain.Accounts;
using Minaev.Domain.Services.Accounts;
using Minaev.Tools.Types;
using Minaev.WebAPI.Infrastructure;
using Minaev.WebAPI.Providers;
using System.IdentityModel.Tokens.Jwt;

namespace Minaev.WebAPI.Middleware;

public class JWTMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public JWTMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task Invoke(HttpContext context, IAccountsService accountsService)
    {
        String? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token is not null) AttachAccountToContext(context, token, accountsService);

        await _next(context);
    }

    private void AttachAccountToContext(HttpContext context, String token, IAccountsService accountsService)
    {
        try
        {
            JwtToken? bannedToken = accountsService.GetBannedToken(token);
            if (bannedToken is not null) throw new Exception("Некорретный токен"); ;

            JwtSecurityTokenHandler tokenHandler = new();
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,

                IssuerSigningKey = Jwt.SecurityKey,
                ValidateIssuerSigningKey = true,
            }, out SecurityToken validatedToken);

            JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
            ID userId = jwtToken.Claims.First(claim => claim.Type == ClaimNames.Id).Value;

            Account? account = accountsService.GetAccount(userId);
            if (account == null) throw new Exception("Некорретный токен");

            context.Items["Account"] = new SystemUser(account.Id, account.Username, token, account.IsAdmin);
        }
        catch
        {
            // do nothing if jwt validation fails
        }
    }
}
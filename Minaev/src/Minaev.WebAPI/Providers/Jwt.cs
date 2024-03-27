using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Minaev.WebAPI.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Minaev.WebAPI.Providers;

public static class Jwt
{
    public static readonly SymmetricSecurityKey SecurityKey = new(Encoding.ASCII.GetBytes("8bDg4vKwRZzPAt5pFJ2NzXrWqDmYt8aM"));

    public static void AddJwtAuthentication(this IServiceCollection services)
    {
        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,

                    IssuerSigningKey = SecurityKey,
                    ValidateIssuerSigningKey = true,
                };
            });
    }

    public static void AddJwtAuthorization(this IServiceCollection services)
    {
        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        services.AddAuthorization(configure =>
        {
            configure.AddPolicy("Admin", (policy) =>
            {
                policy.RequireClaim(ClaimNames.IsAdmin, "True");
            });

        });
    }

    public static void AddSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            String basePath = AppContext.BaseDirectory;

            String xmlPath = Path.Combine(basePath, "Minaev.WebAPI.xml");
            options.IncludeXmlComments(xmlPath);

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Please enter token"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new List<String>()
                }
            });
        });
    }

    public static String Create(Claim[] claims)
    {
        DateTime tokenExpirationDateTime = DateTime.Now.AddDays(7);

        JwtSecurityTokenHandler tokenHandler = new();
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            NotBefore = DateTime.UtcNow,
            Subject = new ClaimsIdentity(claims),
            Expires = tokenExpirationDateTime,
            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature)
        };

        JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
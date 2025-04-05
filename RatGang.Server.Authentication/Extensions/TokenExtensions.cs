using Microsoft.IdentityModel.Tokens;
using RatGang.Server.Authentication.Entety;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RatGang.Server.Authentication.Extensions;

public static class TokenExtensions
{
    public static TokensResponce GetTokensPair(TokensRequest options)
    {
        var accessToken = GetToken(options,
            Configurate.Singleton.LifeTime,
            Configurate.Singleton.GetSymmetricSecurityKey());
        var refreshToken = GetToken(options,
            Configurate.Singleton.RefreshLifeTime,
            Configurate.Singleton.GetRefreshSymmetricSecurityKey());

        return new()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
        };
    }

    public static Guid GetUserId(this IEnumerable<Claim> claims)
    {
        var strUserId = claims.FirstOrDefault(_ => _.Type == "userId")
            ?? throw new ArgumentException("brake_token");

        return Guid.Parse(strUserId.Value);
    }

    public static TokensRequest GetTokensRequest(this IEnumerable<Claim> claims)
    {
        var claimUserId = claims.FirstOrDefault(_ => _.Type == "userId")
            ?? throw new ArgumentException("brake_token");
        var claimRole = claims.FirstOrDefault(_ => _.Type == ClaimTypes.Role)
            ?? throw new ArgumentException("brake_token");

        return new()
        {
            Role = claimRole.Value,
            UserId = Guid.Parse(claimUserId.Value),
        };
    }

    private static string GetToken(TokensRequest options, 
        double expirationMinutes, SecurityKey key)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new("userId", options.UserId.ToString()),
                new(ClaimTypes.Role, options.Role)
            ]),
            IssuedAt = DateTime.UtcNow,
            Issuer = Configurate.Singleton.Issuer,
            Audience = Configurate.Singleton.Audience,
            Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
            SigningCredentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var encodedToken = tokenHandler.WriteToken(token);
        return encodedToken;
    }
}

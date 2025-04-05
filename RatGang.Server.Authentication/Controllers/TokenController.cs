using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RatGang.Server.Authentication.Extensions;

namespace RatGang.Server.Authentication.Controllers;

[ApiController]
[Route("[controller]")]
public class TokenController : Controller
{
    [HttpGet("Refresh")]
    [Authorize("Refresh")]
    public IActionResult RefreshTokens()
    {
        var request = User.Claims.GetTokensRequest();

        var tokens = TokenExtensions.GetTokensPair(request);

        return Ok(tokens);
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RatGang.Server.Authentication.Extensions;
using RatGang.Server.Users.Database.Entety;
using RatGang.Server.Users.Entety.Request.AuthRequests;
using RatGang.Server.Users.Extensions;
using RatGang.Server.Users.Services;

namespace RatGang.Server.Users.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizationController(
    IUserService userService,
    IVerifyService verifyService,
    IAuthenticationService authenticationService) : Controller
{
    [HttpPut("Email/Add")]
    public async Task<IActionResult> AddEmailAuthMethod(Guid userId, EmailAuthRequest options)
    {
        try
        {
            var user = await userService.GetAsync(userId, [UserComponents.UserAuthMethods]);

            await authenticationService.AddEmailAuthMethodAsync(user, options);
            await verifyService.SendVerityEmail(options.Email);

            return Ok($"verify mail sended to {options.Email}");
        }
        catch (NullReferenceException ex) { return NotFound(ex.Message); }
    }

    [HttpGet("Email")]
    public async Task<IActionResult> AuthFromEmail(
        [FromQuery] EmailAuthRequest options)
    {
        if (await authenticationService.AuthWithEmailAsync(options))
        {
            await verifyService.SendVerityEmail(options.Email);
            return Ok($"verify mail sended to {options.Email}");
        }
        return BadRequest("auth_error");
    }

    [HttpGet("Email/Verify")]
    [Authorize("Access")]
    public async Task<IActionResult> ConfirmationAuthFromEmail(
        [FromQuery] string email,
        [FromQuery] string code)
    {
        try
        {
            User user = await verifyService.VerificationEmail(email, code);

            var tokens = TokenExtensions.GetTokensPair(
                User.Claims.GetTokensRequest());
            
            return Ok(user.ToLoginResponce(tokens));
        }
        catch (NullReferenceException ex) { return NotFound(ex.Message); }

    }
}

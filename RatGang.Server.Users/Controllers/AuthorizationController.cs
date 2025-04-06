using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RatGang.Server.Authentication.Extensions;
using RatGang.Server.Users.Database.Entety;
using RatGang.Server.Users.Entety.Request;
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

    [HttpPost("Email")]
    public async Task<IActionResult> RegistrationFromEmail(
        [FromBody] CreateUserRequest options)
    {
        await userService.CreateAsync(options);
        await verifyService.SendVerityEmail(options.Email);

        return Ok($"verify mail sended to {options.Email}");
    }

    [HttpGet("Email/Verify")]
    public async Task<IActionResult> ConfirmationAuthFromEmail(
        [FromQuery] string email,
        [FromQuery] string code)
    {
        try
        {
            User user = await verifyService.VerificationEmail(email, code);

            var tokens = TokenExtensions.GetTokensPair(new()
            {
                Role = user.Role,
                UserId = user.Id,
            });

            return Ok(user.ToLoginResponce(tokens));
        }
        catch (NullReferenceException ex) { return NotFound(ex.Message); }

    }
}

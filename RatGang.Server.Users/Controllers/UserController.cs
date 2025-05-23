﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RatGang.Server.Authentication.Extensions;
using RatGang.Server.Users.Entety.Request;
using RatGang.Server.Users.Extensions;
using RatGang.Server.Users.Services;

namespace RatGang.Server.Users.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(
    IUserService userService) : Controller
{
    [HttpGet]
    [Authorize("Access")]
    public async Task<IActionResult> GetUser()
    {
        var userId = TokenExtensions.GetUserId(
            User.Claims);

        var user = await userService.GetAsync(userId);

        return Ok(user.ToResponce());
    }
}

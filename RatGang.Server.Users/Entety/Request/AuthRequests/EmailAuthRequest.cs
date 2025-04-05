using System.ComponentModel.DataAnnotations;

namespace RatGang.Server.Users.Entety.Request.AuthRequests;

public class EmailAuthRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

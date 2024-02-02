using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace NotificationCenter.Controllers.v1;

[ApiController]
[Route("/v1/account")]
public class AccountController : Controller
{
    [HttpGet("login/oidc")]
    public async Task LoginOidc([FromQuery] string redirectUrl = "/")
    {
        await HttpContext.ChallengeAsync("OpenIdConnect", new AuthenticationProperties
        {
            RedirectUri = redirectUrl,
        });
    }
}

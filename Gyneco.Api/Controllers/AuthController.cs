using Gyneco.Application.Models.Identity;
using Gyneco.Domain.Contracts.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gyneco.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authenticationService;

    public AuthController(IAuthService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
    {
        var response = await _authenticationService.Login(request);
        Response.Cookies.Append("AuthToken", response.Token, new CookieOptions
        {
            HttpOnly = true,
            Expires = response.DateTokenExpiration,
            Path = "/"
        });
        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
    {
        return Ok(await _authenticationService.Register(request));
    }
}
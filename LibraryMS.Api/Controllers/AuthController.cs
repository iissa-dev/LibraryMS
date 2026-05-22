using System.Security.Claims;
using LibraryMS.Application.DTOs.AuthDto;
using LibraryMS.Application.Features.Auth.Commands.Login;
using LibraryMS.Application.Features.Auth.Commands.Logout;
using LibraryMS.Application.Features.Auth.Commands.RefreshToken;
using LibraryMS.Application.Features.Auth.Queries.User;
using LibraryMS.Application.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("LoginAsync")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
    {
        var result = await mediator.Send(new LoginCommand(loginDto.UserName, loginDto.Password));
        if (result.IsFailure) return Unauthorized(new { error = result.Error });

        Response.Cookies.Append("refreshToken", result.Data!.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        });

        return Ok(result);
    }

    [HttpPut("Logout")]
    public async Task<IActionResult> Logout()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if (refreshToken is null) return BadRequest(Result.Failure("Invalid Refresh Token Or you are not login"));

        var result = await mediator.Send(new LogoutCommand(refreshToken));

        Response.Cookies.Delete("refreshToken"); // Clean Revoked Cookies
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut("Refresh")]
    public async Task<IActionResult> RefreshTokenAsync()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if (refreshToken is null) return BadRequest(Result.Failure("Invalid Refresh Token Or you are not login"));

        var result = await mediator.Send(new RefreshTokenCommand(refreshToken));

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("Current")]
    public async Task<IActionResult> GetCurrentUserAsync()
    {
        var usreClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if(usreClaim is null) return Unauthorized(new {error = "User not found or not login"});
        var userId = int.Parse(usreClaim);
        var result = await mediator.Send(new CurrentUserCommand(userId));

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}

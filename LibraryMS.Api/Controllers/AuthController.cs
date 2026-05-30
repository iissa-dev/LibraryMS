using LibraryMS.Application.DTOs.AuthDto;
using LibraryMS.Application.Features.Auth.Commands.Logout;
using LibraryMS.Application.Features.Auth.Commands.RefreshToken;
using LibraryMS.Application.Features.Auth.Queries.Login;
using LibraryMS.Application.Features.Auth.Queries.User;

namespace LibraryMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("LoginAsync")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
    {
        var result = await mediator.Send(new LoginQuery(loginDto.UserName, loginDto.Password));
        if (result.IsFailure) return Unauthorized(new { error = result.Error });

        Response.Cookies.Append(ApiConstant.RefreshTokenKey, result.Data!.RefreshToken, new CookieOptions
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
        var refreshToken = Request.Cookies[ApiConstant.RefreshTokenKey];
        if (refreshToken is null) return BadRequest(Result.Failure("Invalid Refresh Token Or you are not login"));

        var result = await mediator.Send(new LogoutCommand(refreshToken));

        Response.Cookies.Delete(ApiConstant.RefreshTokenKey); // Clean Revoked Cookies
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut("Refresh")]
    public async Task<IActionResult> RefreshTokenAsync()
    {
        var refreshToken = Request.Cookies[ApiConstant.RefreshTokenKey];
        if (refreshToken is null) return BadRequest(Result.Failure("Invalid Refresh Token Or you are not login"));

        var result = await mediator.Send(new RefreshTokenCommand(refreshToken));

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("Current")]
    public async Task<IActionResult> GetCurrentUserAsync()
    {
        var result = await mediator.Send(new CurrentUserQuery(User.GetUserId()));

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}

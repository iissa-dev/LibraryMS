using LibraryMS.Application.DTOs.AuthDto;
using LibraryMS.Application.Features.Auth.Commands.Logout;
using LibraryMS.Application.Features.Auth.Commands.RefreshToken;
using LibraryMS.Application.Features.Auth.Queries.Login;
using LibraryMS.Application.Features.Auth.Queries.User;
namespace LibraryMS.Api.Controllers;

public class AuthController(ISender sender, IWebHostEnvironment hostEnvironment) : BaseController
{
    private void SetRefreshTokenCookie(string refreshToken)
    {
        Response.Cookies.Append(ApiConstant.RefreshTokenKey, refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = hostEnvironment.IsProduction(), // Set Secure flag in production
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        });
    }

    [HttpPost("LoginAsync")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
    {
        var result = await sender.Send(new LoginQuery(loginDto.UserName, loginDto.Password));
        if (result.IsFailure) return Unauthorized(new { error = result.Error });

        SetRefreshTokenCookie(result.Data!.RefreshToken);

        return HandleResult(result);
    }

    [HttpPut("Logout")]
    [AllowAnonymous]
    public async Task<IActionResult> Logout()
    {
        var refreshToken = Request.Cookies[ApiConstant.RefreshTokenKey];
        if (refreshToken is null) return BadRequest(Result.Failure("Invalid Refresh Token Or you are not login"));

        var result = await sender.Send(new LogoutCommand(refreshToken));

        Response.Cookies.Delete(ApiConstant.RefreshTokenKey); // Clean Revoked Cookies
        return HandleResult(result);
    }

    [HttpPut("Refresh")]
    public async Task<IActionResult> RefreshTokenAsync()
    {
        var refreshToken = Request.Cookies[ApiConstant.RefreshTokenKey];
        if (refreshToken is null) return BadRequest(Result.Failure("Invalid Refresh Token Or you are not login"));

        var result = await sender.Send(new RefreshTokenCommand(refreshToken));

        if (result.IsFailure) return BadRequest(result);

        SetRefreshTokenCookie(result.Data!.RefreshToken);

        return HandleResult(result);
    }

    [HttpGet("Current")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCurrentUserAsync()
    {
        var result = await sender.Send(new CurrentUserQuery(User.GetUserId()));

        return HandleResult(result);
    }
}

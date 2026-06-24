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
        var isProd = hostEnvironment.IsProduction();
        Response.Cookies.Append(ApiConstant.RefreshTokenKey, refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = isProd, // Set Secure flag in production
            SameSite = isProd ? SameSiteMode.Strict : SameSiteMode.Lax,
            Expires = DateTime.UtcNow.AddDays(7),
            Path = "/"
        });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
    {
        var result = await sender.Send(new LoginQuery(loginDto.UserName, loginDto.Password));
        if (result.IsFailure) return Unauthorized(result);

        SetRefreshTokenCookie(result.Data!.RefreshToken);

        return HandleResult(result);
    }

    [HttpPut("logout")]
    [AllowAnonymous]
    public async Task<IActionResult> Logout()
    {
        var refreshToken = Request.Cookies[ApiConstant.RefreshTokenKey];
        if (refreshToken is null) return BadRequest(Result.Failure("Invalid Refresh Token Or you are not login"));

        var result = await sender.Send(new LogoutCommand(refreshToken));

        Response.Cookies.Delete(ApiConstant.RefreshTokenKey); // Clean Revoked Cookies
        return HandleResult(result);
    }

    [HttpPost("Refresh")]
    [AllowAnonymous]
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

    [HttpGet("me")]
    [Authorize]
    public IActionResult Me()
    {
        return Ok(new { userId = User.GetUserId(), role = User.GetUserRole(), username = User.GetUserName() });
    }
}

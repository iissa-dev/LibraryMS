namespace LibraryMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    protected IActionResult HandleResult(Result result)
    {
        if (result is null) return StatusCode(500, "Response result is null");

        if (result.IsSuccess) return Ok(result);

        if (result.Error.Contains("not found", StringComparison.OrdinalIgnoreCase))
        {
            return NotFound(result);
        }

        if (result.Error.Contains("unauthorized", StringComparison.OrdinalIgnoreCase))
        {
            return Unauthorized(result);
        }

        return BadRequest(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result == null) return StatusCode(500, "Response result is null");

        if (result.IsSuccess && result.Data != null) return Ok(result);

        if (result.IsSuccess && result.Data == null) return NotFound(result);

        if (result.Error.Contains("not found", StringComparison.OrdinalIgnoreCase))
        {
            return NotFound(result);
        }

        return BadRequest(result);
    }
}
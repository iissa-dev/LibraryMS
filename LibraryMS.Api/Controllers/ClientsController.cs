using LibraryMS.Application.Features.Client.Commands.DeleteClient;
using LibraryMS.Application.Features.Client.Commands.RegisterClient;
using LibraryMS.Application.Features.Client.Commands.RestoreClient;
using LibraryMS.Application.Features.Client.Commands.UpdateClient;
using LibraryMS.Application.Features.Client.Queries.GetAllClient;
using LibraryMS.Application.Features.Client.Queries.GetClientById;
using Microsoft.AspNetCore.Authorization;

namespace LibraryMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(int pageNumber, int pageSize)
    {
        var result = await mediator.Send(new GetAllClientQuery(pageNumber, pageSize));
        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("GetClientProfileByIdUserAsync")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetEmployeeProfileByIdUserAsync()
    {
        var result = await mediator.Send(new GetClientByIdQuery(User.GetUserId()));
        return result.IsFailure ? NotFound(result) : Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterClientCommand command)
    {
        var result = await mediator.Send(command);
        if (result.IsFailure)
            return BadRequest(result);

        return StatusCode(StatusCodes.Status201Created, result);
    }

    [HttpPut($"update-client-info/{{{nameof(userId)}}}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateClientAsynTask([FromRoute] int userId, UpdateClientCommand command)
    {
        if (userId != command.UserId) return BadRequest(Result.Failure("User Id mismatch"));

        var result = await mediator.Send(command);
        if (result.IsFailure) return NotFound("Client profile not found or no changes made");

        return Ok(result);
    }

    [HttpDelete($"delete-client/{{{nameof(userId)}}}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteClientAsync([FromRoute] int userId)
    {
        var result = await mediator.Send(new DeleteClientCommand(userId));

        return result.IsSuccess
        ? Ok(result)
        : NotFound(result);
    }

    [HttpPut($"resotre-client/{{{nameof(userId)}}}")]
    public async Task<IActionResult> RestoreClientAsync([FromRoute]int userId)
    {
        var result = await mediator.Send(new RestoreClientCommand(userId));

        return result.IsSuccess
        ? Ok(result)
        : NotFound(result);
    }
}
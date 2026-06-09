using LibraryMS.Application.Features.Client.Commands.DeleteClient;
using LibraryMS.Application.Features.Client.Commands.RegisterClient;
using LibraryMS.Application.Features.Client.Commands.RestoreClient;
using LibraryMS.Application.Features.Client.Commands.UpdateClient;
using LibraryMS.Application.Features.Client.Queries.GetAllClient;
using LibraryMS.Application.Features.Client.Queries.GetClientById;

namespace LibraryMS.Api.Controllers;

public class ClientsController(ISender sender, IAuthorizationService authService) : BaseController
{
    [HttpGet]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> Get(int pageNumber, int pageSize)
    {
        var result = await sender.Send(new GetAllClientQuery(pageNumber, pageSize));
        return HandleResult(result);
    }

    [HttpGet("get-client-profile/{clientId}")]
    [Authorize]
    public async Task<IActionResult> GetClientProfileByIdUserAsync(int clientId)
    {
        var authorizationResult = await authService.AuthorizeAsync(User, clientId, new EntityAccessRequirement());
        if (!authorizationResult.Succeeded)
            return Forbid();

        var result = await sender.Send(new GetClientByIdQuery(clientId));
        return HandleResult(result);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterClientCommand command)
    {
        var result = await sender.Send(command);
        if (result.IsFailure)
            return BadRequest(result);

        return HandleResult(result);
    }

    [HttpPut("update-client-info/{clientId:int}")]
    [Authorize]
    public async Task<IActionResult> UpdateClientAsynTask([FromRoute] int clientId, UpdateClientCommand command)
    {
        if (clientId != command.ClientId) return BadRequest(Result.Failure("Client Id mismatch"));

        var authorizationResult = await authService.AuthorizeAsync(User, clientId, new EntityAccessRequirement());
        if (!authorizationResult.Succeeded)
            return Forbid();

        var result = await sender.Send(command);
        return HandleResult(result);
    }

    [HttpDelete("delete-client/{clientId:int}/user/{userId:int}")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> DeleteClientAsync([FromRoute] int clientId, [FromRoute] int userId)
    {
        var result = await sender.Send(new DeleteClientCommand(userId, clientId));
        return HandleResult(result);
    }

    [HttpPut("resotre-client/{clientId:int}/user/{userId:int}")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> RestoreClientAsync([FromRoute] int userId, [FromRoute] int clientId)
    {
        var result = await sender.Send(new RestoreClientCommand(userId, clientId));
        return HandleResult(result);
    }
}
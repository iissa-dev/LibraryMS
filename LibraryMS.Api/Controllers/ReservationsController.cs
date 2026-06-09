using LibraryMS.Application.Features.Reservations.Commands.Cancel;
using LibraryMS.Application.Features.Reservations.Commands.Fulfill;
using LibraryMS.Application.Features.Reservations.Commands.Reserve;
using LibraryMS.Application.Features.Reservations.Queries.GetById;

namespace LibraryMS.Api.Controllers;

public class ReservationsController(ISender sender, IAuthorizationService authService) : BaseController
{
    [HttpPost("reserve")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> ReserveAsync([FromBody] ReserveCommand command)
    {
        var authorizationResult = await authService.AuthorizeAsync(User, command.ClientId, new EntityAccessRequirement());
        if (!authorizationResult.Succeeded)
            return Forbid();

        var result = await sender.Send(command);
        return HandleResult(result);
    }

    [HttpPut("{reserveId:int}/cancel")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> CancelAsync([FromRoute] int reserveId)
    {
        var result = await sender.Send(new CancelCommand(reserveId));
        return HandleResult(result);
    }

    [HttpPut("{reserveId:int}/fulfill")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> FulFillAsync([FromRoute] int reserveId)
    {
        var result = await sender.Send(new FulfillReservationCommand(reserveId));
        return HandleResult(result);
    }

    [HttpGet("client/{clientId:int}")]
    [Authorize]
    public async Task<IActionResult> GetAllClientReservationAsync([FromRoute] int clientId)
    {
        var result = await sender.Send(new GetAllClientReservationQuery(clientId));
        return HandleResult(result);
    }
}
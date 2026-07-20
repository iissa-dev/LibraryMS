using LibraryMS.Application.Features.Reservations.Commands.Cancel;
using LibraryMS.Application.Features.Reservations.Commands.Fulfill;
using LibraryMS.Application.Features.Reservations.Commands.Reserve;
using LibraryMS.Application.Features.Reservations.Queries.GetById;

namespace LibraryMS.Api.Controllers;

public class ReservationsController(ISender sender, IAuthorizationService authService) : BaseController
{
    [HttpPost("reserve")]
    [Authorize]
    public async Task<IActionResult> ReserveAsync([FromBody] ReserveCommand command)
    {
        var authResult = await authService.AuthorizeAsync(User, command.ClientId, new EntityAccessRequirement());
        if (!authResult.Succeeded) return Forbid();

        var result = await sender.Send(command);
        return HandleResult(result);
    }

    [HttpPut("cancel")]
    [Authorize]
    public async Task<IActionResult> CancelAsync([FromBody] CancelCommand command)
    {
        var authResult = await authService.AuthorizeAsync(User, command.ClientId, new EntityAccessRequirement());
        if (!authResult.Succeeded) return Forbid();

        var result = await sender.Send(command);
        return HandleResult(result);
    }

    [HttpPut("fulfill")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> FulFillAsync([FromBody] FulfillReservationCommand command)
    {
        var result = await sender.Send(command);
        return HandleResult(result);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllClientReservationAsync([FromQuery] GetAllClientReservationQuery query)
    {
        var result = await sender.Send(query);
        return HandleResult(result);
    }
}
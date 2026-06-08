using LibraryMS.Application.Features.Reservations.Commands.Cancel;
using LibraryMS.Application.Features.Reservations.Commands.Fulfill;
using LibraryMS.Application.Features.Reservations.Commands.Reserve;
using LibraryMS.Application.Features.Reservations.Queries.GetById;

namespace LibraryMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController(ISender sender) : ControllerBase
{
    [HttpPost("reserve")]
    public async Task<IActionResult> ReserveAsync([FromBody] ReserveCommand command)
    {
        var result = await sender.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{reserveId:int}/cancel")]
    public async Task<IActionResult> CancelAsync([FromRoute] int reserveId)
    {
        var result = await sender.Send(new CancelCommand(reserveId));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{reserveId:int}/fulfill")]
    public async Task<IActionResult> FulFillAsync([FromRoute] int reserveId)
    {
        var result = await sender.Send(new FulfillReservationCommand(reserveId));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("client/{clientId:int}")]
    public async Task<IActionResult> GetAllClientReservationAsync([FromRoute] int clientId)
    {
        var result = await sender.Send(new GetAllClientReservationQuery(clientId));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
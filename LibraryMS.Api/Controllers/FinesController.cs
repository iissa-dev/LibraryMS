using LibraryMS.Application.Features.Fine.Commands.PayFine;
using LibraryMS.Application.Features.Fine.Queries.CheckFineStatus;
using LibraryMS.Application.Features.Fine.Queries.GetById;

namespace LibraryMS.Api.Controllers;

public class FinesController(ISender sender, IAuthorizationService authService) : BaseController
{
    [HttpGet("client/{clientId}")]
    [Authorize]
    public async Task<IActionResult> GetAllFinesById([FromRoute] int clientId, [FromQuery] GetAllFinesByIdQuery query)
    {
        var authorizationResult = await authService.AuthorizeAsync(User, clientId, new EntityAccessRequirement());
        if (!authorizationResult.Succeeded)
            return Forbid();

        if (clientId != query.ClientId) return BadRequest(Result.Failure("Mismatch ClientId"));

        var result = await sender.Send(query);
        return HandleResult(result);
    }

    [HttpGet("{borrowingId:int}/check")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> CheckFineStatusAsync([FromRoute] int borrowingId)
    {
        var result = await sender.Send(new CheckFineStatusQuery(borrowingId));
        return HandleResult(result);
    }

    [HttpPut("{fineId:int}/pay")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> PayFineAsync([FromRoute] int fineId)
    {
        var result = await sender.Send(new PayFineCommand(fineId));
        return HandleResult(result);
    }
}
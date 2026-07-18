using LibraryMS.Application.Features.Fine.Commands.PayFine;
using LibraryMS.Application.Features.Fine.Queries.CheckFineStatus;
using LibraryMS.Application.Features.Fine.Queries.GetById;

namespace LibraryMS.Api.Controllers;

public class FinesController(ISender sender) : BaseController
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllFinesById([FromQuery] GetAllFinesByIdQuery query)
    {
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
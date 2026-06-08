using LibraryMS.Application.Features.Fine.Commands.PayFine;
using LibraryMS.Application.Features.Fine.Queries.CheckFineStatus;
using LibraryMS.Application.Features.Fine.Queries.GetById;

namespace LibraryMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class FinesController(ISender sender) : ControllerBase
{
    [HttpGet("client/{clientId}")]
    public async Task<IActionResult> GetAllfinesById(int clientId, GetAllfinesByIdQuery query)
    {
        if (clientId != query.ClientId) return BadRequest(Result.Failure("Mismatch ClientId"));
        var result = await sender.Send(query);
        return result.IsSuccess
        ? Ok(result)
        : BadRequest(result);
    }

    [HttpGet("{borrowingId:int}/check")]
    public async Task<IActionResult> CheckFineStatusAsync([FromRoute] int borrowingId)
    {
        var result = await sender.Send(new CheckFineStatusQuery(borrowingId));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{fineId:int}/pay")]
    public async Task<IActionResult> PayFineAsync([FromRoute] int fineId)
    {
        var result = await sender.Send(new PayFineCommand(fineId));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
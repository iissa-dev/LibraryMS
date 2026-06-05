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
}
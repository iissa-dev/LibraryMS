using LibraryMS.Application.Features.Borrowing.Commands.Create;
using LibraryMS.Application.Features.Borrowing.Commands.Return;
using LibraryMS.Application.Features.Borrowing.Queries.GetFullBorrowDetailsById;

namespace LibraryMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BorrowsController(ISender sender) : ControllerBase
{
    [HttpPost("borrow")]
    public async Task<IActionResult> CreateBorrow(CreateBorrowingsCommand command)
    {
        var result = await sender.Send(command);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpPost("return")]
    public async Task<IActionResult> ReturnBorrow(ReturnBorrowingsCommand command)
    {
        var result = await sender.Send(command);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpGet("client/{clientId}")]
    public async Task<IActionResult> GetFullBorrowDetails(int clientId, GetFullBorrowDetailsQuery query)
    {
        if (clientId != query.ClientId) return BadRequest(Result.Failure("Mismatch ClientId"));
        var result = await sender.Send(query);
        return result.IsSuccess
        ? Ok(result)
        : BadRequest(result);
    }
}
using LibraryMS.Application.Features.Borrowing.Commands.Create;
using LibraryMS.Application.Features.Borrowing.Commands.Return;
using LibraryMS.Application.Features.Borrowing.Queries.GetFullBorrowDetailsById;

namespace LibraryMS.Api.Controllers;

public class BorrowsController(ISender sender) : BaseController
{
    [HttpPost("borrow")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> CreateBorrow(CreateBorrowingsCommand command)
    {
        var result = await sender.Send(command);
        return HandleResult(result);
    }

    [HttpPost("return")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> ReturnBorrow(ReturnBorrowingsCommand command)
    {
        var result = await sender.Send(command);
        return HandleResult(result);
    }

    [HttpGet("get-full-borrow-details")]
    // [Authorize]
    public async Task<IActionResult> GetFullBorrowDetails([FromQuery] GetFullBorrowDetailsQuery query)
    {
        var result = await sender.Send(query);
        return HandleResult(result);
    }
}
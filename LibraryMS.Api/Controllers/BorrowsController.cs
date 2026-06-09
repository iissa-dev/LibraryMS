using LibraryMS.Application.Features.Borrowing.Commands.Create;
using LibraryMS.Application.Features.Borrowing.Commands.Return;
using LibraryMS.Application.Features.Borrowing.Queries.GetFullBorrowDetailsById;
using Microsoft.AspNetCore.Authorization;

namespace LibraryMS.Api.Controllers;

public class BorrowsController(ISender sender, IAuthorizationService authService) : BaseController
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

    [HttpGet("client/{clientId}")]
    [Authorize]
    public async Task<IActionResult> GetFullBorrowDetails(int clientId, GetFullBorrowDetailsQuery query)
    {
        if (clientId != query.ClientId) return BadRequest(Result.Failure("Mismatch ClientId"));

        var authorizationResult = await authService.AuthorizeAsync(User, clientId, new EntityAccessRequirement());
        if (!authorizationResult.Succeeded)
            return Forbid();

        var result = await sender.Send(query);
        return HandleResult(result);
    }
}
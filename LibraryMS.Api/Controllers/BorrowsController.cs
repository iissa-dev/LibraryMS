using LibraryMS.Application.Features.Borrowing.Commands.Create;
using LibraryMS.Application.Features.Borrowing.Commands.Return;
using LibraryMS.Application.Features.Borrowing.Queries.GetFullBorrowDetailsById;
using LibraryMS.Domain.Enums;

namespace LibraryMS.Api.Controllers;

public class BorrowsController(ISender sender, IAuthorizationService authorizationService) : BaseController
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
    [Authorize]
    public async Task<IActionResult> GetFullBorrowDetails([FromQuery] GetFullBorrowDetailsQuery query)
    {
        var role = User.GetUserRole();
        if (role == nameof(Roles.Client))
        {
            var auth = await authorizationService.AuthorizeAsync(User, query.ClientId, new EntityAccessRequirement());
            if (!auth.Succeeded) return Forbid();
        }

        var result = await sender.Send(query);
        return HandleResult(result);
    }
}
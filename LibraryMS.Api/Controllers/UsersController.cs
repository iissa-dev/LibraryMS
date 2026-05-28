using LibraryMS.Api.Common.Extensions;
using LibraryMS.Application.Features.Person.Commands.UpdatePersonInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMS.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpPut("update-my-profile")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateMyProfile([FromBody] UpdatePersonInfoCommand command)
    {
        var newCommand = command with {UserId = User.GetUserId()};

        var result = await mediator.Send(newCommand);
        return result.IsSuccess? Ok(result) : BadRequest(result);
    }
}
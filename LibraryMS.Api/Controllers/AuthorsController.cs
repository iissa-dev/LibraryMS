using LibraryMS.Application.Features.Author.Commands.Create;
using LibraryMS.Application.Features.Author.Commands.Delete;
using LibraryMS.Application.Features.Author.Commands.Restore;
using LibraryMS.Application.Features.Author.Commands.Update;
using LibraryMS.Application.Features.Author.Queries.GetAuthorById;
using LibraryMS.Application.Features.Author.Queries.GetAuthors;

namespace LibraryMS.Api.Controllers;

public class AuthorsController(ISender sender) : BaseController
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateAuthorCommand command)
    {
        var result = await sender.Send(command);
        return HandleResult(result);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await sender.Send(new GetAuthorByIdQuery(id));
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await sender.Send(new DeleteAuthorCommand(id));
        return HandleResult(result);
    }

    [HttpPut("{id}/restore-author")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Restore(int id)
    {
        var result = await sender.Send(new RestoreAuthorCommand(id));
        return HandleResult(result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, UpdateAuthorCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(Result.Failure("Id in URL does not match Id in request body."));
        }
        var result = await sender.Send(command);
        return HandleResult(result);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAuthors([FromQuery] GetAuthorsQuery query)
    {
        var result = await sender.Send(query);
        return HandleResult(result);
    }
}
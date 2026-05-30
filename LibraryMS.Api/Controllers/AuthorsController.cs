using LibraryMS.Application.Features.Author.Commands.Create;
using LibraryMS.Application.Features.Author.Commands.Delete;
using LibraryMS.Application.Features.Author.Commands.Restore;
using LibraryMS.Application.Features.Author.Commands.Update;
using LibraryMS.Application.Features.Author.Queries.GetAuthorById;
using LibraryMS.Application.Features.Author.Queries.GetAuthors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateAuthorCommand command)
    {
        var result = await mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetAuthorByIdQuery(id));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await mediator.Send(new DeleteAuthorCommand(id));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{id}/restore-author")]
    public async Task<IActionResult> Restore(int id)
    {
        var result = await mediator.Send(new RestoreAuthorCommand(id));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateAuthorCommand command)
    {
        if(id != command.Id)
        {
            return BadRequest("Id in URL does not match Id in request body.");
        }
        var result = await mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAuthors([FromQuery] GetAuthorsQuery query)
    {
        var result = await mediator.Send(query);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
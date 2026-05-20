using LibraryMS.Application.Features.Book.Commands.CreateBook;
using LibraryMS.Application.Features.Book.Queries.GetAllBook;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("books")]
    public async Task<IActionResult> GetAllBooks([FromQuery] GetAllBooksQuery query)
    {
        var result = await _mediator.Send(query);
        if(result.IsFailure)
            return BadRequest(result);
        
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddBook([FromBody]CreateBookCommand command)
    {
        var result = await _mediator.Send(command);
        
        if(result.IsFailure)
            return BadRequest(result);
        
        return StatusCode(StatusCodes.Status201Created, result);
    }
    
}
using LibraryMS.Application.Features.Book.Commands.CreateBook;
using LibraryMS.Application.Features.Book.Commands.DeleteBook;
using LibraryMS.Application.Features.Book.Commands.RestoreBook;
using LibraryMS.Application.Features.Book.Commands.UpdateBook;
using LibraryMS.Application.Features.Book.Queries.GetAllBook;
using LibraryMS.Application.Features.Book.Queries.GetByIdWithAuthors;
using LibraryMS.Application.Features.BookCopies.Command.Create;
using LibraryMS.Application.Features.BookCopies.Command.Delete;
using LibraryMS.Application.Features.BookCopies.Command.Restore;
using LibraryMS.Application.Features.BookCopies.Command.UpdateStatus;
using LibraryMS.Application.Features.BookCopies.Queries.GetAllCopies;

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

    [HttpGet]
    public async Task<IActionResult> GetAllBooks([FromQuery] GetAllBooksQuery query)
    {
        var result = await _mediator.Send(query);
        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddBook([FromBody] CreateBookCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(result);

        return StatusCode(StatusCodes.Status201Created, result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook([FromRoute] int id)
    {
        var result = await _mediator.Send(new DeleteBookCommand(id));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut("restore/{id}")]
    public async Task<IActionResult> RestoreBook([FromRoute] int id)
    {
        var result = await _mediator.Send(new RestoreBookCommand(id));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetByIdWithAuthorsQuery(id));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] UpdateBookCommand command)
    {
        if (id != command.Id)
            return BadRequest("ID mismatch.");

        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("copies")]
    public async Task<IActionResult> GetAllCopies([FromQuery] GetAllCopiesQuery query)
    {
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("{bookId:int}/copies")]
    public async Task<IActionResult> InsertNewCopy([FromRoute] int bookId, [FromBody] CreateBookCopyCommand command)
    {
        var updatedCommand = command with { BookId = bookId };

        var result = await _mediator.Send(updatedCommand);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{bookId:int}/delete-copy")]
    public async Task<IActionResult> DeleteCopy([FromRoute] int bookId)
    {
        var result = await _mediator.Send(new DeleteCopyCommand(bookId));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPatch("{copyId:int}/update-status")]
    public async Task<IActionResult> UpdateStatus([FromRoute] int copyId, [FromBody] UpdateStatusCopyCommand command)
    {
        var updatedCommand = command with { BookCopyId = copyId };
        var result = await _mediator.Send(updatedCommand);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{copyId:int}/restore-copy")]
    public async Task<IActionResult> RestoreCopyAsync([FromRoute] int copyId)
    {
        var result = await _mediator.Send(new RestoreCopyCommand(copyId));
        return result.IsSuccess ? Ok(result) : BadRequest(Request);
    }
}
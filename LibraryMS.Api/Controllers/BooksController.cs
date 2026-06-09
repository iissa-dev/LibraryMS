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
using Microsoft.AspNetCore.Authorization;

namespace LibraryMS.Api.Controllers;

[Authorize(Roles = "Admin,Employee")]
public class BooksController(ISender sender) : BaseController
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllBooks([FromQuery] GetAllBooksQuery query)
    {
        var result = await sender.Send(query);
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody] CreateBookCommand command)
    {
        var result = await sender.Send(command);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook([FromRoute] int id)
    {
        var result = await sender.Send(new DeleteBookCommand(id));
        return HandleResult(result);
    }

    [HttpPut("restore/{id}")]
    public async Task<IActionResult> RestoreBook([FromRoute] int id)
    {
        var result = await sender.Send(new RestoreBookCommand(id));
        return HandleResult(result);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetBookById([FromRoute] int id)
    {
        var result = await sender.Send(new GetByIdWithAuthorsQuery(id));
        return HandleResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] UpdateBookCommand command)
    {
        if (id != command.Id)
            return BadRequest("ID mismatch.");

        var result = await sender.Send(command);
        return HandleResult(result);
    }

    [HttpGet("copies")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllCopies([FromQuery] GetAllCopiesQuery query)
    {
        var result = await sender.Send(query);
        return HandleResult(result);
    }

    [HttpPost("{bookId:int}/copies")]
    public async Task<IActionResult> InsertNewCopy([FromRoute] int bookId, [FromBody] CreateBookCopyCommand command)
    {
        var updatedCommand = command with { BookId = bookId };

        var result = await sender.Send(updatedCommand);

        return HandleResult(result);
    }

    [HttpDelete("{bookId:int}/delete-copy")]
    public async Task<IActionResult> DeleteCopy([FromRoute] int bookId)
    {
        var result = await sender.Send(new DeleteCopyCommand(bookId));
        return HandleResult(result);
    }

    [HttpPatch("{copyId:int}/update-status")]
    public async Task<IActionResult> UpdateStatus([FromRoute] int copyId, [FromBody] UpdateStatusCopyCommand command)
    {
        var updatedCommand = command with { BookCopyId = copyId };
        var result = await sender.Send(updatedCommand);
        return HandleResult(result);
    }

    [HttpPut("{copyId:int}/restore-copy")]
    public async Task<IActionResult> RestoreCopyAsync([FromRoute] int copyId)
    {
        var result = await sender.Send(new RestoreCopyCommand(copyId));
        return HandleResult(result);
    }
}
using LibraryMS.Application.Common.Results;
using MediatR;

namespace LibraryMS.Application.Features.Book.Commands.DeleteBook;

public sealed record DeleteBookCommand(int Id) : IRequest<Result>;

using LibraryMS.Application.Common.Results;
using MediatR;

namespace LibraryMS.Application.Features.Author.Commands.Delete;

public sealed record DeleteAuthorCommand(int Id) : IRequest<Result>;

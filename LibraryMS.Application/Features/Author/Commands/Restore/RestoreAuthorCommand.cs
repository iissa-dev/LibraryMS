using LibraryMS.Application.Common.Results;
using MediatR;

namespace LibraryMS.Application.Features.Author.Commands.Restore;

public sealed record RestoreAuthorCommand(int Id) : IRequest<Result>;
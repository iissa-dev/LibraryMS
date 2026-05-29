using LibraryMS.Application.Common.Results;
using MediatR;

namespace LibraryMS.Application.Features.Client.Commands.RestoreClient;

public sealed record RestoreClientCommand(int UserId) : IRequest<Result>;

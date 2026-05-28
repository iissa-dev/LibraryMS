using LibraryMS.Application.Common.Results;
using MediatR;

namespace LibraryMS.Application.Features.Auth.Commands.Logout;

public sealed record LogoutCommand(string RefreshToken) : IRequest<Result>;

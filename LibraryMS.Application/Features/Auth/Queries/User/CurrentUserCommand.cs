using LibraryMS.Application.DTOs.AuthDto;
using LibraryMS.Application.Results;
using MediatR;

namespace LibraryMS.Application.Features.Auth.Queries.User;

public record CurrentUserCommand(int UserId) : IRequest<Result<CurrentUserDto>>;

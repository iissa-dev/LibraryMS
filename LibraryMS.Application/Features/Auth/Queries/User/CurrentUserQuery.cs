using LibraryMS.Application.Common.Results;
using LibraryMS.Application.DTOs.AuthDto;
using MediatR;

namespace LibraryMS.Application.Features.Auth.Queries.User;

public record CurrentUserQuery(int UserId) : IRequest<Result<CurrentUserDto>>;

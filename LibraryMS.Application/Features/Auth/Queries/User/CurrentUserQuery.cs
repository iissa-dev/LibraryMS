using LibraryMS.Application.Common.DTOs.AuthDto;
using LibraryMS.Application.Common.Results;
using MediatR;

namespace LibraryMS.Application.Features.Auth.Queries.User;

public record CurrentUserQuery(int UserId) : IRequest<Result<CurrentUserDto>>;

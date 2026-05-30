using LibraryMS.Application.DTOs.AuthDto;

namespace LibraryMS.Application.Features.Auth.Queries.User;

public record CurrentUserQuery(int UserId) : IRequest<Result<CurrentUserDto>>;

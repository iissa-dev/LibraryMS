namespace LibraryMS.Application.Features.Account_Management.Commands.Update;

public sealed record UpdateAccountCommand(
    int UserId,
    string UserName,
    string Email,
    string? PhoneNumber
) : IRequest<Result>;

public sealed class UpdateAccountCommandHandler(IIdentityUser identityUser)
    : IRequestHandler<UpdateAccountCommand, Result>
{
    public async Task<Result> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        return await identityUser.UpdateUserInfoAsync(new DTOs.UserDto.UpdateUserInfoDto
        {
            UserId = request.UserId,
            UserName = request.UserName,
            Email = request.Email,
            PhoneNumber = request?.PhoneNumber
        });
    }
}

public sealed class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
{
    public UpdateAccountCommandValidator()
    {
        RuleFor(u => u.UserId)
            .GreaterThan(0)
            .WithMessage("User Id must be valid Id and greater than zero");

        RuleFor(u => u.UserName)
            .NotEmpty()
            .WithMessage("User name is requried");

        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("Email is requried")
            .EmailAddress()
            .WithMessage("It is not valid Email");
    }
}
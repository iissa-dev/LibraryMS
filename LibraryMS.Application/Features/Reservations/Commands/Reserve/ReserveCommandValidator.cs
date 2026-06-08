namespace LibraryMS.Application.Features.Reservations.Commands.Reserve;

public sealed class ReserveCommandValidator : AbstractValidator<ReserveCommand>
{
    public ReserveCommandValidator()
    {
        RuleFor(b => b.ClientId)
            .GreaterThan(0)
            .WithMessage("Client Id must be a valid ID greater than 0");

        RuleFor(b => b.BookId)
            .GreaterThan(0)
            .WithMessage("Book Id must be a valid ID greater than 0");
    }
}
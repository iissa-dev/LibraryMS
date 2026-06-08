namespace LibraryMS.Application.Features.Reservations.Queries.GetById;

public sealed class GetAllClientReservationQueryValidator : AbstractValidator<GetAllClientReservationQuery>
{
    public GetAllClientReservationQueryValidator()
    {
        RuleFor(r => r.ClientId)
            .GreaterThan(0)
            .WithMessage("Client Id must be valid Id and Greater than zero");
    }
}
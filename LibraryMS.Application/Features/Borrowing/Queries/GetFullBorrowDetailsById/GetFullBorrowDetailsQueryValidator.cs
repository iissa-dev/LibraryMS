namespace LibraryMS.Application.Features.Borrowing.Queries.GetFullBorrowDetailsById;

public sealed class GetFullBorrowDetailsQueryValidator : AbstractValidator<GetFullBorrowDetailsQuery>
{
    public GetFullBorrowDetailsQueryValidator()
    {
        RuleFor(b => b.PageNumber).GreaterThan(0).WithMessage("Page number should be greater then zero");
        RuleFor(b => b.PageSize).LessThanOrEqualTo(100).WithMessage("Page size should be less than 100");
        RuleFor(b => b.ClientId).GreaterThan(0).WithMessage("Client ID must be a valid positive integer.");
    }
}
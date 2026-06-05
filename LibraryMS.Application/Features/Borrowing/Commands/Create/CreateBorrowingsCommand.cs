namespace LibraryMS.Application.Features.Borrowing.Commands.Create;

public sealed record CreateBorrowingsCommand(
    int CopyId,
    int ClientId
) : IRequest<Result>;
